using AutoMapper;
using Events.Application.Models;
using Events.Domain.Entities;
using Events.Domain.Models;
using FluentValidation;
using Events.Application.Interfaces;
using Events.Domain.Interfaces;
using Serilog;
using Events.Application.Exceptions;

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

        public ServiceResponse<List<Event>> GetAllAsync()
        {
            var entities = _eventsRepository.GetAllAsync()
                    .ToList();

            Log.Information("Events were successfully received");

            return new ServiceResponse<List<Event>>
            {
                Success = true,
                Data = _mapper.Map<List<Event>>(entities)
            };
        }

        public async Task<ServiceResponse<List<Participant>>> GetParticipantsAsync(Guid eventId)
        {
            var entity = await _eventsRepository.GetAsync(eventId);

            if (entity is not null)
            {
                Log.Information("Participants were successfully received");

                return new ServiceResponse<List<Participant>>
                {
                    Success = true,
                    Data = _mapper.Map<List<Participant>>(entity.Participants)
                };
            }

            throw new ClientException("Event was not found", 400);
        }

        public async Task<ServiceResponse<Event?>> GetByIdAsync(Guid id)
        {
            var entity = await _eventsRepository.GetAsync(id);

            Log.Information("The event has been successfully received");

            return new ServiceResponse<Event?>
            {
                Success = true,
                Data = _mapper.Map<Event>(entity)
            };
        }

        public ServiceResponse<Event?> GetByTitleAsync(string title)
        {
            var entity = _eventsRepository.GetAllAsync()
                    .FirstOrDefault(x => x.Title.ToLower().Contains(title.ToLower()));

            Log.Information("The event has been successfully received");

            return new ServiceResponse<Event?>
            {
                Success = true,
                Data = _mapper.Map<Event>(entity)
            };
        }

        public ServiceResponse<Event?> GetByDateAsync(DateTime date)
        {
            var entity = _eventsRepository.GetAllAsync()
                    .FirstOrDefault(x => x.HoldedAt == date);

            Log.Information("The event has been successfully received");

            return new ServiceResponse<Event?>
            {
                Success = true,
                Data = _mapper.Map<Event>(entity)
            };
        }

        public ServiceResponse<Event?> GetByVenueAsync(string venue)
        {
            var entity = _eventsRepository.GetAllAsync()
                    .FirstOrDefault(x => x.Venue.ToLower() == venue.ToLower());

            Log.Information("The event has been successfully received");

            return new ServiceResponse<Event?>
            {
                Success = true,
                Data = _mapper.Map<Event>(entity)
            };
        }

        public ServiceResponse<Event?> GetByCategoryAsync(Category category)
        {
            var entity = _eventsRepository.GetAllAsync()
                    .FirstOrDefault(x => x.Category == category);

            Log.Information("The event has been successfully received");

            return new ServiceResponse<Event?>
            {
                Success = true,
                Data = _mapper.Map<Event>(entity)
            };
        }

        public async Task<ServiceResponse<Event>> AddAsync(EventRequest model)
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

            Log.Information("The event has been successfully added");

            return new ServiceResponse<Event>
            {
                Success = true,
                Data = _mapper.Map<Event>(entity)
            };
        }

        public async Task<ServiceResponse<Event>> UpdateAsync(Guid id, EventRequest model)
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

                Log.Information("The event has been successfully updated");

                _participantService.ReportTheChanges(entity);

                return new ServiceResponse<Event>
                {
                    Success = true,
                    Data = _mapper.Map<Event>(entity)
                };
            }

            throw new ClientException("Event was not found", 400);
        }

        public async Task<ServiceResponse<Event>> DeleteAsync(Guid id)
        {
            var entity = await _eventsRepository.GetAsync(id);

            if (entity is not null)
            {
                await _eventsRepository.DeleteAsync(entity);

                Log.Information("The event has been successfully deleted");

                return new ServiceResponse<Event>
                {
                    Success = true,
                    Data = _mapper.Map<Event>(entity)
                };
            }

            throw new ClientException("Event was not found", 400);
        }
    }
}
