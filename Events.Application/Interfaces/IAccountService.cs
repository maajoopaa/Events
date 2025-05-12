using Events.Application.Models;

namespace Events.Application.Interfaces
{
    public interface IAccountService
    {
        ServiceResponse<AccountResponse> Login(ParticipantLoginRequest model);
        Task<ServiceResponse<AccountResponse>> Register(ParticipantRegisterRequest model);
    }
}