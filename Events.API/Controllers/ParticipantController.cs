using Events.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Events.API.Controllers
{
    [ApiController]
    [Route("api/participant")]
    public class ParticipantController : ControllerBase
    {
        private readonly IParticipantService _participantService;

        public ParticipantController(IParticipantService participantService)
        {
            _participantService = participantService;
        }

        [HttpPatch("{id:guid}"), Authorize]
        public async Task<IActionResult> AbolitionParticipationAsync(Guid id)
        {
            var response = await _participantService.AbolitionParticipationAsync(id);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpPatch("{id:guid}/{eventId:guid}"), Authorize]
        public async Task<IActionResult> RegisterParticipationAsync(Guid id, Guid eventId)
        {
            var response = await _participantService.RegisterParticipationAsync(id, eventId);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var response = await _participantService.GetByIdAsync(id);

            return response.Success ? Ok(response.Data) : BadRequest(response.Error);
        }
    }
}
