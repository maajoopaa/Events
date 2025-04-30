using Events.Application.Models;
using Events.Domain.Models;

namespace Events.Application.Services
{
    public interface IEventService
    {
        Task<ServiceResponse<Event>> AddAsync(EventRequest model);
        Task<ServiceResponse<Event>> DeleteAsync(Guid id);
        Task<ServiceResponse<List<Event>>> GetAllAsync();
        Task<ServiceResponse<Event?>> GetByCategoryAsync(Category category);
        Task<ServiceResponse<Event?>> GetByDateAsync(DateTime date);
        Task<ServiceResponse<Event?>> GetByIdAsync(Guid id);
        Task<ServiceResponse<Event?>> GetByTitleAsync(string title);
        Task<ServiceResponse<Event?>> GetByVenueAsync(string venue);
        Task<ServiceResponse<List<Participant>>> GetParticipantsAsync(Guid eventId);
        Task<ServiceResponse<Event>> UpdateAsync(Guid id, EventRequest model);
    }
}