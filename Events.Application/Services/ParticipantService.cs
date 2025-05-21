using AutoMapper;
using Events.Application.Models;
using Events.Domain.Models;
using System.Net.Mail;
using System.Net;
using Events.Domain.Entities;
using Events.Application.Services.AccountService;
using Events.Application.Interfaces;
using Events.Domain.Interfaces;
using Serilog.Core;
using Serilog;
using Events.Application.Exceptions;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using FluentValidation;

namespace Events.Application.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IMapper _mapper;
        private readonly IParticipantsRepository _participantsRepository;
        private readonly IValidator<ParticipantRegisterRequest> _regValidator;

        public ParticipantService(IEventsRepository eventsRepository, IMapper mapper, IParticipantsRepository participantsRepository,
            IValidator<ParticipantRegisterRequest> regValidator)
        {
            _eventsRepository = eventsRepository;
            _mapper = mapper;
            _participantsRepository = participantsRepository;
            _regValidator = regValidator;
        }

        public async Task<ServiceResponse<ParticipantEntity>> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            var entity = await _participantsRepository.GetAsync(email, cancellationToken);

            Log.Information("The participant has been successfully received");

            return new ServiceResponse<ParticipantEntity>
            {
                Success = true,
                Data = entity
            };
        }

        public async Task<ServiceResponse<Participant>> AddAsync(ParticipantRegisterRequest model, CancellationToken cancellationToken = default)
        {
            await _regValidator.ValidateAndThrowAsync(model, cancellationToken);

            var entity = new ParticipantEntity
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth,
                DateOfRegistry = DateTime.UtcNow,
                Email = model.Email,
                PasswordHash = PasswordHasher.Hash(model.Password)
            };

            try
            {
                await _participantsRepository.AddAsync(entity, cancellationToken);
            }
            catch(InvalidOperationException ex) when 
                (ex.InnerException is NpgsqlException npgEx)
            {
                throw new ClientException("A participant with such an email already exists", 400);
            }

            Log.Information("The participant has been successfully added");

            return new ServiceResponse<Participant>
            {
                Success = true,
                Data = _mapper.Map<Participant>(entity)
            };
        }

        public async Task<ServiceResponse<Participant>> RegisterParticipationAsync(Guid participantId, Guid eventId,
            CancellationToken cancellationToken = default)
        {
            var participantEntity = await _participantsRepository.GetAsync(participantId, cancellationToken);

            if (participantEntity is not null)
            {
                var eventEntity = await _eventsRepository.GetAsync(eventId, cancellationToken);

                if (eventEntity is not null)
                {
                    participantEntity.Event = eventEntity;

                    await _participantsRepository.UpdateAsync(participantEntity, cancellationToken);

                    Log.Information("Participation was successfully registered");

                    return new ServiceResponse<Participant>
                    {
                        Success = true,
                        Data = _mapper.Map<Participant>(participantEntity)
                    };
                }

                throw new ClientException("Event was not found", 400);
            }

            throw new ClientException("Participant was not found", 400);
        }

        public async Task<ServiceResponse<Participant>> AbolitionParticipationAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _participantsRepository.GetAsync(id, cancellationToken);

            if (entity is not null)
            {
                entity.Event = null;

                await _participantsRepository.UpdateAsync(entity, cancellationToken);

                Log.Information("Participation was successfully canceled");

                return new ServiceResponse<Participant>
                {
                    Success = true,
                    Data = _mapper.Map<Participant>(entity)
                };
            }

            throw new ClientException("Participant was not found", 400);
        }

        public async Task<ServiceResponse<Participant>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _participantsRepository.GetAsync(id, cancellationToken);

            Log.Information("The participant has been successfully received");

            return new ServiceResponse<Participant>
            {
                Success = true,
                Data = _mapper.Map<Participant>(entity)
            };
        }

        public void ReportTheChanges(EventEntity entity, CancellationToken cancellationToken)
        {
            if (entity is not null)
            {
                SmtpClient smtpClient = new SmtpClient("smtp.mail.ru")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("maqd@list.ru", "quPTnRJrfmW1b2PprUmW"),
                    EnableSsl = true
                };

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("maqd@list.ru"),
                    Subject = "Event was changed",
                    Body = $"Date: {entity.HoldedAt.ToLongDateString()} {entity.HoldedAt.ToLongTimeString()}\nVenue: {entity.Venue}",
                    IsBodyHtml = false
                };

                foreach (var participant in entity.Participants)
                {
                    Task.Run(() =>
                    {
                        mailMessage.To.Add(participant.Email);

                        smtpClient.Send(mailMessage);

                        Log.Information("Mail has been successfully sended");
                    }, cancellationToken);
                }
            }
        }
    }
}
