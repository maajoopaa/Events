using Events.Application.Interfaces;
using Events.Application.Models;
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
        public async Task<IActionResult> LoginAsync([FromBody] ParticipantLoginRequest model, CancellationToken cancellationToken)
        {
            var response = await _accountService.LoginAsync(model, cancellationToken);

            return Ok(response.Data);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] ParticipantRegisterRequest model, CancellationToken cancellationToken)
        {
            var response = await _accountService.RegisterAsync(model, cancellationToken);

            return Ok(response.Data);
        }
    }
}
