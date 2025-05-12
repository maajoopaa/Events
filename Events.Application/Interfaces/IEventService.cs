using Events.Application.Models;
using Events.Domain.Models;

namespace Events.Application.Interfaces
{
    public interface IEventService
    {
        Task<ServiceResponse<Event>> AddAsync(EventRequest model);
        Task<ServiceResponse<Event>> DeleteAsync(Guid id);
        ServiceResponse<List<Event>> GetAllAsync();
        ServiceResponse<Event?> GetByCategoryAsync(Category category);
        ServiceResponse<Event?> GetByDateAsync(DateTime date);
        Task<ServiceResponse<Event?>> GetByIdAsync(Guid id);
        ServiceResponse<Event?> GetByTitleAsync(string title);
        ServiceResponse<Event?> GetByVenueAsync(string venue);
        Task<ServiceResponse<List<Participant>>> GetParticipantsAsync(Guid eventId);
        Task<ServiceResponse<Event>> UpdateAsync(Guid id, EventRequest model);
    }
}