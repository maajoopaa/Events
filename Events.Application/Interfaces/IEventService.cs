using Events.Application.Models;
using Events.Domain.Models;

namespace Events.Application.Interfaces
{
    public interface IEventService
    {
        Task<ServiceResponse<Event>> AddAsync(EventRequest model, CancellationToken cancellationToken = default);
        Task<ServiceResponse<Event>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        ServiceResponse<List<Event>> GetAll();
        ServiceResponse<Event?> GetByCategory(Category category);
        ServiceResponse<Event?> GetByDate(DateTime date);
        Task<ServiceResponse<Event?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        ServiceResponse<Event?> GetByTitle(string title);
        ServiceResponse<Event?> GetByVenue(string venue);
        Task<ServiceResponse<List<Participant>>> GetParticipantsAsync(Guid eventId, CancellationToken cancellationToken = default);
        Task<ServiceResponse<Event>> UpdateAsync(Guid id, EventRequest model, CancellationToken cancellationToken = default);
    }
}