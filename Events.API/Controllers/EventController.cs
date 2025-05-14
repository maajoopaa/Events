using Events.Application.Interfaces;
using Events.Application.Models;
using Events.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Events.API.Controllers
{
    [ApiController]
    [Route("api/event")] 
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("list")]
        public async Task<IActionResult> GetAllAsync([FromBody] PaginationModel model, CancellationToken cancellationToken)
        {
            var response = await _eventService.GetAllAsync(model, cancellationToken);

            return Ok(response.Data);
        }

        [HttpGet("{id:guid}/participants")]
        public async Task<IActionResult> GetParticipantsAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = await _eventService.GetParticipantsAsync(id, cancellationToken);

            return Ok(response.Data);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = await _eventService.GetByIdAsync(id, cancellationToken);

            return Ok(response.Data);
        }

        [HttpPost("title/{title}")]
        public async Task<IActionResult> GetByTitleAsync(string title, [FromBody] PaginationModel model, CancellationToken cancellationToken)
        {
            var response = await _eventService.GetByTitleAsync(title, model,cancellationToken);

            return Ok(response.Data);
        }

        [HttpPost("date/{date:datetime}")]
        public async Task<IActionResult> GetByDateAsync(DateTime date, [FromBody] PaginationModel model, CancellationToken cancellationToken)
        {
            var response = await _eventService.GetByDateAsync(date, model, cancellationToken);

            return Ok(response.Data);
        }

        [HttpPost("venue/{venue}")]
        public async Task<IActionResult> GetByVenueAsync(string venue, [FromBody] PaginationModel model, CancellationToken cancellationToken)
        {
            var response = await _eventService.GetByVenueAsync(venue, model, cancellationToken);

            return Ok(response.Data);
        }

        [HttpPost("category/{category:int}")]
        public async Task<IActionResult> GetByCategoryAsync(int category, [FromBody] PaginationModel model, CancellationToken cancellationToken)
        {
            var response = await _eventService.GetByCategoryAsync((Category)category, model, cancellationToken);

            return Ok(response.Data);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AddAsync([FromBody] EventRequest model, CancellationToken cancellationToken)
        {
            var response = await _eventService.AddAsync(model, cancellationToken);

            return Ok(response.Data);
        }

        [HttpPut("{id:guid}"), Authorize]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] EventRequest model, CancellationToken cancellationToken)
        {
            var response = await _eventService.UpdateAsync(id, model, cancellationToken);

            return Ok(response.Data);
        }

        [HttpDelete("{id:guid}"), Authorize]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var response = await _eventService.DeleteAsync(id, cancellationToken);

            return Ok(response.Data);
        }
    }
}
