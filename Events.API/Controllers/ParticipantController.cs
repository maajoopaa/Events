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
        public async Task<IActionResult> AbolitionParticipationAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = await _participantService.AbolitionParticipationAsync(id,cancellationToken);

            return Ok(response.Data);
        }

        [HttpPatch("{id:guid}/{eventId:guid}"), Authorize]
        public async Task<IActionResult> RegisterParticipationAsync(Guid id, Guid eventId, CancellationToken cancellationToken)
        {
            var response = await _participantService.RegisterParticipationAsync(id, eventId, cancellationToken);

            return Ok(response.Data);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = await _participantService.GetByIdAsync(id, cancellationToken);

            return Ok(response.Data);
        }
    }
}
