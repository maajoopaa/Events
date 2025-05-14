using Events.Application.Models;
using Events.Domain.Entities;
using Events.Domain.Models;

namespace Events.Application.Interfaces
{
    public interface IParticipantService
    {
        Task<ServiceResponse<Participant>> AbolitionParticipationAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResponse<Participant>> AddAsync(ParticipantRegisterRequest model, CancellationToken cancellationToken = default);
        ServiceResponse<ParticipantEntity> GetByEmail(string email);
        Task<ServiceResponse<Participant>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ServiceResponse<Participant>> RegisterParticipationAsync(Guid participantId, Guid eventId, CancellationToken cancellationToken = default);
        void ReportTheChanges(EventEntity entity, CancellationToken cancellationToken);
    }
}