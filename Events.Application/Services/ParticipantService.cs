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

namespace Events.Application.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IMapper _mapper;
        private readonly IParticipantsRepository _participantsRepository;

        public ParticipantService(IEventsRepository eventsRepository, IMapper mapper, IParticipantsRepository participantsRepository)
        {
            _eventsRepository = eventsRepository;
            _mapper = mapper;
            _participantsRepository = participantsRepository;
        }

        public ServiceResponse<ParticipantEntity> GetByEmailAsync(string email)
        {
            var entity = _participantsRepository.GetAllAsync()
                    .FirstOrDefault(x => x.Email == email);

            Log.Information("The participant has been successfully received");

            return new ServiceResponse<ParticipantEntity>
            {
                Success = true,
                Data = entity
            };
        }

        public async Task<ServiceResponse<Participant>> AddAsync(ParticipantRegisterRequest model)
        {
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
                await _participantsRepository.AddAsync(entity);
            }
            catch(DbUpdateException ex) when 
                (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
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

        public async Task<ServiceResponse<Participant>> RegisterParticipationAsync(Guid participantId, Guid eventId)
        {
            var participantEntity = await _participantsRepository.GetAsync(participantId);

            if (participantEntity is not null)
            {
                var eventEntity = await _eventsRepository.GetAsync(eventId);

                if (eventEntity is not null)
                {
                    participantEntity.Event = eventEntity;

                    await _participantsRepository.UpdateAsync(participantEntity);

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

        public async Task<ServiceResponse<Participant>> AbolitionParticipationAsync(Guid id)
        {
            var entity = await _participantsRepository.GetAsync(id);

            if (entity is not null)
            {
                entity.Event = null;

                await _participantsRepository.UpdateAsync(entity);

                Log.Information("Participation was successfully canceled");

                return new ServiceResponse<Participant>
                {
                    Success = true,
                    Data = _mapper.Map<Participant>(entity)
                };
            }

            throw new ClientException("Participant was not found", 400);
        }

        public async Task<ServiceResponse<Participant>> GetByIdAsync(Guid id)
        {
            var entity = await _participantsRepository.GetAsync(id);

            Log.Information("The participant has been successfully received");

            return new ServiceResponse<Participant>
            {
                Success = true,
                Data = _mapper.Map<Participant>(entity)
            };
        }

        public void ReportTheChanges(EventEntity entity)
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
                    });
                }
            }
        }
    }
}
