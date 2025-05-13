using AutoMapper;
using Events.Application.Exceptions;
using Events.Application.Interfaces;
using Events.Application.Models;
using Events.Domain.Models;
using FluentValidation;
using Serilog;

namespace Events.Application.Services.AccountService
{
    public class AccountService : IAccountService
    {
        private readonly IParticipantService _participantService;
        private readonly IValidator<ParticipantRegisterRequest> _regValidator;
        private readonly IValidator<ParticipantLoginRequest> _loginValidator;
        private readonly IMapper _mapper;

        public AccountService(IParticipantService participantService, IValidator<ParticipantRegisterRequest> regValidator,
            IValidator<ParticipantLoginRequest> loginValidator, IMapper mapper)
        {
            _participantService = participantService;
            _regValidator = regValidator;
            _loginValidator = loginValidator;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AccountResponse>> Register(ParticipantRegisterRequest model)
        {
            var result = _regValidator.Validate(model);

            var response = await _participantService.AddAsync(model);

            if (response.Success)
            {
                return Login(new ParticipantLoginRequest
                {
                    Email = model.Email,
                    Password = model.Password
                });
            }

            throw new ClientException("Account with this email already exists", 400);
        }

        public ServiceResponse<AccountResponse> Login(ParticipantLoginRequest model)
        {
            var response = _participantService.GetByEmailAsync(model.Email);

            if (response.Data is not null && PasswordHasher.Verify(model.Password, response.Data.PasswordHash))
            {
                var domain = _mapper.Map<Participant>(response.Data);

                var token = JwtService.GenerateToken(domain);

                Log.Information("The user has been successfully entered the system");

                return new ServiceResponse<AccountResponse>
                {
                    Success = true,
                    Data = new AccountResponse
                    {
                        Token = token,
                        Account = domain
                    }
                };
            }

            throw new ClientException("Incorrect email or password", 400);
        }
    }
}
