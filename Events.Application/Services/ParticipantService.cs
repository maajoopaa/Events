using AutoMapper;
using Events.Application.Models;
using Events.DataAccess.Repositories;
using Events.Domain.Models;
using System.Net.Mail;
using System.Net;
using Events.Domain.Entities;
using Events.Application.Services.AccountService;

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

        public async Task<ServiceResponse<ParticipantEntity>> GetByEmailAsync(string email)
        {
            try
            {
                var entity = (await _participantsRepository.GetAllAsync())
                    .FirstOrDefault(x => x.Email == email);

                return new ServiceResponse<ParticipantEntity>
                {
                    Success = true,
                    Data = entity
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<ParticipantEntity>
                {
                    Error = ex.Message,
                };
            }
        }

        public async Task<ServiceResponse<Participant>> AddAsync(ParticipantRegisterRequest model)
        {
            try
            {
                var entity = new ParticipantEntity
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    DateOfBirth = model.DateOfBirth,
                    DateOfRegistry = DateTime.UtcNow,
                    Email = model.Email,
                    Password = PasswordHasher.Hash(model.Password)
                };

                await _participantsRepository.AddAsync(entity);

                return new ServiceResponse<Participant>
                {
                    Success = true,
                    Data = _mapper.Map<Participant>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Participant>
                {
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<Participant>> RegisterParticipationAsync(Guid participantId, Guid eventId)
        {
            try
            {
                var participantEntity = await _participantsRepository.GetAsync(participantId);

                if (participantEntity is not null)
                {
                    var eventEntity = await _eventsRepository.GetAsync(eventId);

                    if (eventEntity is not null)
                    {
                        participantEntity.Event = eventEntity;

                        await _participantsRepository.UpdateAsync(participantEntity);

                        return new ServiceResponse<Participant>
                        {
                            Success = true,
                            Data = _mapper.Map<Participant>(participantEntity)
                        };
                    }

                    return new ServiceResponse<Participant>
                    {
                        Error = "Event was not found"
                    };
                }

                return new ServiceResponse<Participant>
                {
                    Error = "Participant was not found"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Participant>
                {
                    Error = ex.Message,
                };
            }
        }

        public async Task<ServiceResponse<Participant>> AbolitionParticipationAsync(Guid id)
        {
            try
            {
                var entity = await _participantsRepository.GetAsync(id);

                if (entity is not null)
                {
                    entity.Event = null;

                    await _participantsRepository.UpdateAsync(entity);

                    return new ServiceResponse<Participant>
                    {
                        Success = true,
                        Data = _mapper.Map<Participant>(entity)
                    };
                }

                return new ServiceResponse<Participant>
                {
                    Error = "Participant was not found"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Participant>
                {
                    Error = ex.Message,
                };
            }
        }

        public async Task<ServiceResponse<Participant>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _participantsRepository.GetAsync(id);

                return new ServiceResponse<Participant>
                {
                    Success = true,
                    Data = _mapper.Map<Participant>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Participant>
                {
                    Error = ex.Message,
                };
            }
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
                    });
                }
            }
        }
    }
}
