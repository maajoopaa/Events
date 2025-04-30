using Events.Application.Models;

namespace Events.Application.Services.AccountService
{
    public interface IAccountService
    {
        Task<ServiceResponse<AccountResponse>> Login(ParticipantLoginRequest model);
        Task<ServiceResponse<AccountResponse>> Register(ParticipantRegisterRequest model);
    }
}