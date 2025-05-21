using AutoMapper;
using Events.Application.Models;
using Events.Domain.Entities;
using Events.Domain.Models;
using FluentValidation;
using Events.Application.Interfaces;
using Events.Domain.Interfaces;
using Serilog;
using Events.Application.Exceptions;
using System.Linq.Expressions;

namespace Events.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<EventRequest> _validator;
        private readonly IParticipantService _participantService;
        private readonly IValidator<PaginationModel> _pageValidator;

        public EventService(IEventsRepository eventsRepository, IMapper mapper, IValidator<EventRequest> validator,
            IParticipantService participantService, IValidator<PaginationModel> pageValidator)
        {
            _eventsRepository = eventsRepository;
            _mapper = mapper;
            _validator = validator;
            _participantService = participantService;
            _pageValidator = pageValidator;
        }

        public async Task<ServiceResponse<List<Event>>> GetAllAsync(PaginationModel model, CancellationToken cancellationToken)
        {
            await _pageValidator.ValidateAndThrowAsync(model, cancellationToken);

            var entities = _eventsRepository.GetAllAsync(x => true, model.Page, model.PageSize, cancellationToken);

            Log.Information("Events were successfully received");

            return new ServiceResponse<List<Event>>
            {
                Success = true,
                Data = _mapper.Map<List<Event>>(entities)
            };
        }

        public async Task<ServiceResponse<List<Participant>>> GetParticipantsAsync(Guid eventId, CancellationToken cancellationToken = default)
        {
            var entity = await _eventsRepository.GetAsync(eventId, cancellationToken);

            if (entity is not null)
            {
                Log.Information("Participants were successfully received");

                return new ServiceResponse<List<Participant>>
                {
                    Success = true,
                    Data = _mapper.Map<List<Participant>>(entity.Participants)
                };
            }

            throw new NotFoundException("Event was not found", 404);
        }

        public async Task<ServiceResponse<Event>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _eventsRepository.GetAsync(id, cancellationToken);

            if(entity is null)
            {
                throw new NotFoundException("Event was not found", 404);
            }

            Log.Information("The event has been successfully received");

            return new ServiceResponse<Event>
            {
                Success = true,
                Data = _mapper.Map<Event>(entity)
            };
        }

        public async Task<ServiceResponse<List<Event>>> GetByTitleAsync(string title, PaginationModel model, CancellationToken cancellationToken)
        {
            await _pageValidator.ValidateAndThrowAsync(model, cancellationToken);

            var entities = _eventsRepository.GetAllAsync(x => x.Title.ToLower().Contains(title.ToLower()), model.Page, model.PageSize,
                cancellationToken);

            Log.Information("The events has been successfully received");

            return new ServiceResponse<List<Event>>
            {
                Success = true,
                Data = _mapper.Map<List<Event>>(entities)
            };
        }

        public async Task<ServiceResponse<List<Event>>> GetByDateAsync(DateTime date, PaginationModel model, CancellationToken cancellationToken)
        {
            await _pageValidator.ValidateAndThrowAsync(model, cancellationToken);

            var entities = _eventsRepository.GetAllAsync(x => x.HoldedAt == date, model.Page, model.PageSize, cancellationToken);

            Log.Information("The events has been successfully received");

            return new ServiceResponse<List<Event>>
            {
                Success = true,
                Data = _mapper.Map<List<Event>>(entities)
            };
        }

        public async Task<ServiceResponse<List<Event>>> GetByVenueAsync(string venue, PaginationModel model, CancellationToken cancellationToken)
        {
            await _pageValidator.ValidateAndThrowAsync(model, cancellationToken);

            var entities = _eventsRepository.GetAllAsync(x => x.Venue.ToLower() == venue.ToLower(), model.Page, model.PageSize, cancellationToken);

            Log.Information("The events has been successfully received");

            return new ServiceResponse<List<Event>>
            {
                Success = true,
                Data = _mapper.Map<List<Event>>(entities)
            };
        }

        public async Task<ServiceResponse<List<Event>>> GetByCategoryAsync(Category category, PaginationModel model, CancellationToken cancellationToken)
        {
            await _pageValidator.ValidateAndThrowAsync(model, cancellationToken);

            var entities = _eventsRepository.GetAllAsync(x => x.Category == category, model.Page, model.PageSize, cancellationToken);

            Log.Information("The events has been successfully received");

            return new ServiceResponse<List<Event>>
            {
                Success = true,
                Data = _mapper.Map<List<Event>>(entities)
            };
        }

        public async Task<ServiceResponse<Event>> AddAsync(EventRequest model, CancellationToken cancellationToken = default)
        {
            await _validator.ValidateAndThrowAsync(model, cancellationToken);

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

            await _eventsRepository.AddAsync(entity, cancellationToken);

            Log.Information("The event has been successfully added");

            return new ServiceResponse<Event>
            {
                Success = true,
                Data = _mapper.Map<Event>(entity)
            };
        }

        public async Task<ServiceResponse<Event>> UpdateAsync(Guid id, EventRequest model, CancellationToken cancellationToken = default)
        {
            await _validator.ValidateAndThrowAsync(model, cancellationToken);

            var entity = await _eventsRepository.GetAsync(id, cancellationToken);

            if (entity is not null)
            {
                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.HoldedAt = model.HoldedAt;
                entity.Venue = model.Venue;
                entity.Category = model.Category;
                entity.MaxCountOfParticipant = model.MaxCountOfParticipant;
                entity.Image = model.Image;

                await _eventsRepository.UpdateAsync(entity, cancellationToken);

                Log.Information("The event has been successfully updated");

                _participantService.ReportTheChanges(entity, cancellationToken);

                return new ServiceResponse<Event>
                {
                    Success = true,
                    Data = _mapper.Map<Event>(entity)
                };
            }

            throw new NotFoundException("Event was not found", 404);
        }

        public async Task<ServiceResponse<Event>> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var entity = await _eventsRepository.GetAsync(id, cancellationToken);

            if (entity is not null)
            {
                await _eventsRepository.DeleteAsync(entity, cancellationToken);

                Log.Information("The event has been successfully deleted");

                return new ServiceResponse<Event>
                {
                    Success = true,
                    Data = _mapper.Map<Event>(entity)
                };
            }

            throw new NotFoundException("Event was not found", 404);
        }
    }
}
