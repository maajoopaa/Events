using AutoMapper;
using Events.Application.Models;
using Events.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            try
            {
                _regValidator.ValidateAndThrow(model);

                var response = await _participantService.AddAsync(model);

                if (response.Success)
                {
                    return await Login(new ParticipantLoginRequest
                    {
                        Email = model.Email,
                        Password = model.Password
                    });
                }

                return new ServiceResponse<AccountResponse>
                {
                    Error = "Account with this email already exists"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<AccountResponse>
                {
                    Error = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<AccountResponse>> Login(ParticipantLoginRequest model)
        {
            try
            {
                var response = await _participantService.GetByEmailAsync(model.Email);

                if (response.Data is not null && PasswordHasher.Verify(model.Password, response.Data.Password))
                {
                    var domain = _mapper.Map<Participant>(response.Data);

                    var token = JwtService.GenerateToken(domain);

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

                return new ServiceResponse<AccountResponse>
                {
                    Error = "Incorrect email or password"
                };
            }
            catch (Exception ex)
            {
                return new ServiceResponse<AccountResponse>
                {
                    Error = ex.Message
                };
            }
        }
    }
}
