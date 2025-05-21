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
        private readonly IValidator<ParticipantLoginRequest> _loginValidator;
        private readonly IMapper _mapper;

        public AccountService(IParticipantService participantService, IValidator<ParticipantLoginRequest> loginValidator, IMapper mapper)
        {
            _participantService = participantService;
            _loginValidator = loginValidator;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AccountResponse>> RegisterAsync(ParticipantRegisterRequest model, CancellationToken cancellationToken = default)
        {
            var response = await _participantService.AddAsync(model, cancellationToken);

            if (response.Success)
            {
                return await LoginAsync(new ParticipantLoginRequest
                {
                    Email = model.Email,
                    Password = model.Password
                });
            }

            throw new DataException("Account with this email already exists", 400);
        }

        public async Task<ServiceResponse<AccountResponse>> LoginAsync(ParticipantLoginRequest model, CancellationToken cancellationToken = default)
        {
            await _loginValidator.ValidateAndThrowAsync(model, cancellationToken);

            var response = await _participantService.GetByEmailAsync(model.Email, cancellationToken);

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

            throw new DataException("Incorrect email or password", 400);
        }
    }
}
