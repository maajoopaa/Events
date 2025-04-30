using AutoMapper;
using Events.Application.Models;
using Events.DataAccess.Repositories;
using Events.Domain.Entities;
using Events.Domain.Models;
using FluentValidation;

namespace Events.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<EventRequest> _validator;
        private readonly IParticipantService _participantService;

        public EventService(IEventsRepository eventsRepository, IMapper mapper, IValidator<EventRequest> validator,
            IParticipantService participantService)
        {
            _eventsRepository = eventsRepository;
            _mapper = mapper;
            _validator = validator;
            _participantService = participantService;
        }

        public async Task<ServiceResponse<List<Event>>> GetAllAsync()
        {
            try
            {
                var entities = await _eventsRepository.GetAllAsync();

                return new ServiceResponse<List<Event>>
                {
                    Success = true,
                    Data = _mapper.Map<List<Event>>(entities)
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<Event>>
                {
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<List<Participant>>> GetParticipantsAsync(Guid eventId)
        {
            try
            {
                var entity = await _eventsRepository.GetAsync(eventId);

                if (entity is not null)
                {
                    return new ServiceResponse<List<Participant>>
                    {
                        Success = true,
                        Data = _mapper.Map<List<Participant>>(entity.Participants)
                    };
                }

                return new ServiceResponse<List<Participant>>
                {
                    Error = "Event was not found"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<List<Participant>>
                {
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<Event?>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _eventsRepository.GetAsync(id);

                return new ServiceResponse<Event?>
                {
                    Success = true,
                    Data = _mapper.Map<Event>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Event?>
                {
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<Event?>> GetByTitleAsync(string title)
        {
            try
            {
                var entites = await _eventsRepository.GetAllAsync();

                var entity = entites.FirstOrDefault(x => x.Title.ToLower() == title.ToLower());

                return new ServiceResponse<Event?>
                {
                    Success = true,
                    Data = _mapper.Map<Event>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Event?>
                {
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<Event?>> GetByDateAsync(DateTime date)
        {
            try
            {
                var entites = await _eventsRepository.GetAllAsync();

                var entity = entites.FirstOrDefault(x => x.HoldedAt == date);

                return new ServiceResponse<Event?>
                {
                    Success = true,
                    Data = _mapper.Map<Event>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Event?>
                {
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<Event?>> GetByVenueAsync(string venue)
        {
            try
            {
                var entites = await _eventsRepository.GetAllAsync();

                var entity = entites.FirstOrDefault(x => x.Venue.ToLower() == venue.ToLower());

                return new ServiceResponse<Event?>
                {
                    Success = true,
                    Data = _mapper.Map<Event>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Event?>
                {
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<Event?>> GetByCategoryAsync(Category category)
        {
            try
            {
                var entites = await _eventsRepository.GetAllAsync();

                var entity = entites.FirstOrDefault(x => x.Category == category);

                return new ServiceResponse<Event?>
                {
                    Success = true,
                    Data = _mapper.Map<Event>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Event?>
                {
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<Event>> AddAsync(EventRequest model)
        {
            try
            {
                _validator.ValidateAndThrow(model);

                var entity = new EventEntity
                {
                    Title = model.Title,
                    Description = model.Description,
                    HoldedAt = model.HoldedAt,
                    Venue = model.Venue,
                    Category = model.Category,
                    MaxCountOfParticipant = model.MaxCountOfParticipant,
                    Image = model.Image,
                };

                await _eventsRepository.AddAsync(entity);

                return new ServiceResponse<Event>
                {
                    Success = true,
                    Data = _mapper.Map<Event>(entity)
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Event>
                {
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<Event>> UpdateAsync(Guid id, EventRequest model)
        {
            try
            {
                _validator.ValidateAndThrow(model);

                var entity = await _eventsRepository.GetAsync(id);

                if (entity is not null)
                {
                    entity.Title = model.Title;
                    entity.Description = model.Description;
                    entity.HoldedAt = model.HoldedAt;
                    entity.Venue = model.Venue;
                    entity.Category = model.Category;
                    entity.MaxCountOfParticipant = model.MaxCountOfParticipant;
                    entity.Image = model.Image;

                    await _eventsRepository.UpdateAsync(entity);

                    _participantService.ReportTheChanges(entity);

                    return new ServiceResponse<Event>
                    {
                        Success = true,
                        Data = _mapper.Map<Event>(entity)
                    };
                }

                return new ServiceResponse<Event>
                {
                    Error = "Event was not found"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Event>
                {
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<Event>> DeleteAsync(Guid id)
        {
            try
            {
                var entity = await _eventsRepository.GetAsync(id);

                if (entity is not null)
                {
                    await _eventsRepository.DeleteAsync(entity);

                    return new ServiceResponse<Event>
                    {
                        Success = true,
                        Data = _mapper.Map<Event>(entity)
                    };
                }

                return new ServiceResponse<Event>
                {
                    Error = "Event was not found"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<Event>
                {
                    Error = ex.Message
                };
            }
        }
    }
}
