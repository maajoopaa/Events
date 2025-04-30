using Events.Domain.Entities;

namespace Events.DataAccess.Repositories
{
    public interface IParticipantsRepository
    {
        Task<ParticipantEntity> AddAsync(ParticipantEntity entity);
        Task<Guid> DeleteAsync(ParticipantEntity entity);
        Task<List<ParticipantEntity>> GetAllAsync();
        Task<ParticipantEntity?> GetAsync(Guid id);
        Task<ParticipantEntity> UpdateAsync(ParticipantEntity entity);
    }
}