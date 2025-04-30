using Events.Application.Models;
using Events.Domain.Entities;
using Events.Domain.Models;

namespace Events.Application.Services
{
    public interface IParticipantService
    {
        Task<ServiceResponse<Participant>> AbolitionParticipationAsync(Guid id);
        Task<ServiceResponse<Participant>> AddAsync(ParticipantRegisterRequest model);
        Task<ServiceResponse<ParticipantEntity>> GetByEmailAsync(string email);
        Task<ServiceResponse<Participant>> GetByIdAsync(Guid id);
        Task<ServiceResponse<Participant>> RegisterParticipationAsync(Guid participantId, Guid eventId);
        void ReportTheChanges(EventEntity entity);
    }
}