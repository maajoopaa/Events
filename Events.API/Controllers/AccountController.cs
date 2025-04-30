using Events.Application.Models;
using Events.Application.Services.AccountService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Events.API.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] ParticipantLoginRequest model)
        {
            var response = await _accountService.Login(model);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] ParticipantRegisterRequest model)
        {
            var response = await _accountService.Register(model);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }
    }
}
