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

        [HttpGet("list")]
        public IActionResult GetAll()
        {
            var response = _eventService.GetAll();

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

        [HttpGet("title/{title}")]
        public IActionResult GetByTitle(string title)
        {
            var response = _eventService.GetByTitle(title);

            return Ok(response.Data);
        }

        [HttpGet("date/{date:datetime}")]
        public IActionResult GetByDate(DateTime date)
        {
            var response = _eventService.GetByDate(date);

            return Ok(response.Data);
        }

        [HttpGet("venue/{venue}")]
        public IActionResult GetByVenue(string venue)
        {
            var response = _eventService.GetByVenue(venue);

            return Ok(response.Data);
        }

        [HttpGet("category/{category:int}")]
        public IActionResult GetByCategory(int category)
        {
            var response = _eventService.GetByCategory((Category)category);

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
