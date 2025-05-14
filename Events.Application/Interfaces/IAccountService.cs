using Events.Application.Models;

namespace Events.Application.Interfaces
{
    public interface IAccountService
    {
        Task<ServiceResponse<AccountResponse>> LoginAsync(ParticipantLoginRequest model, CancellationToken cancellationToken = default);
        Task<ServiceResponse<AccountResponse>> RegisterAsync(ParticipantRegisterRequest model, CancellationToken cancellationToken = default);
    }
}