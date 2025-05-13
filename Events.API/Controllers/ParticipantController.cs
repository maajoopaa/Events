using Events.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

            return Ok(response.Data);
        }

        [HttpPatch("{id:guid}/{eventId:guid}"), Authorize]
        public async Task<IActionResult> RegisterParticipationAsync(Guid id, Guid eventId)
        {
            var response = await _participantService.RegisterParticipationAsync(id, eventId);

            return Ok(response.Data);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var response = await _participantService.GetByIdAsync(id);

            return Ok(response.Data);
        }
    }
}
