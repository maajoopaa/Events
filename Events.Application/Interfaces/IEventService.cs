using Events.Application.Models;
using Events.Domain.Models;

namespace Events.Application.Interfaces
{
    public interface IEventService
    {
        Task<ServiceResponse<Event>> AddAsync(EventRequest model, CancellationToken cancellationToken = default);
        Task<ServiceResponse<Event>> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResponse<List<Event>>> GetAllAsync(PaginationModel model, CancellationToken cancellationToken);
        Task<ServiceResponse<List<Event>>> GetByCategoryAsync(Category category, PaginationModel model, CancellationToken cancellationToken);
        Task<ServiceResponse<List<Event>>> GetByDateAsync(DateTime date, PaginationModel model, CancellationToken cancellationToken);
        Task<ServiceResponse<Event?>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResponse<List<Event>>> GetByTitleAsync(string title, PaginationModel model, CancellationToken cancellationToken);
        Task<ServiceResponse<List<Event>>> GetByVenueAsync(string venue, PaginationModel model, CancellationToken cancellationToken);
        Task<ServiceResponse<List<Participant>>> GetParticipantsAsync(Guid eventId, CancellationToken cancellationToken = default);
        Task<ServiceResponse<Event>> UpdateAsync(Guid id, EventRequest model, CancellationToken cancellationToken = default);
    }
}