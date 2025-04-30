using Events.Domain.Entities;

namespace Events.DataAccess.Repositories
{
    public interface IEventsRepository
    {
        Task<EventEntity> AddAsync(EventEntity entity);
        Task<Guid> DeleteAsync(EventEntity entity);
        Task<List<EventEntity>> GetAllAsync();
        Task<EventEntity?> GetAsync(Guid id);
        Task<EventEntity> UpdateAsync(EventEntity entity);
    }
}