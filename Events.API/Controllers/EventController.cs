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
        public IActionResult GetAllAsync()
        {
            var response = _eventService.GetAllAsync();

            return Ok(response.Data);
        }

        [HttpGet("{id:guid}/participants")]
        public async Task<IActionResult> GetParticipantsAsync(Guid id)
        {
            var response = await _eventService.GetParticipantsAsync(id);

            return Ok(response.Data);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var response = await _eventService.GetByIdAsync(id);

            return Ok(response.Data);
        }

        [HttpGet("title/{title}")]
        public IActionResult GetByTitleAsync(string title)
        {
            var response = _eventService.GetByTitleAsync(title);

            return Ok(response.Data);
        }

        [HttpGet("date/{date:datetime}")]
        public IActionResult GetByTitleAsync(DateTime date)
        {
            var response = _eventService.GetByDateAsync(date);

            return Ok(response.Data);
        }

        [HttpGet("venue/{venue}")]
        public IActionResult GetByVenueAsync(string venue)
        {
            var response = _eventService.GetByVenueAsync(venue);

            return Ok(response.Data);
        }

        [HttpGet("category/{category:int}")]
        public IActionResult GetByCategoryAsync(int category)
        {
            var response = _eventService.GetByCategoryAsync((Category)category);

            return Ok(response.Data);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> AddAsync([FromBody] EventRequest model)
        {
            var response = await _eventService.AddAsync(model);

            return Ok(response.Data);
        }

        [HttpPut("{id:guid}"), Authorize]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] EventRequest model)
        {
            var response = await _eventService.UpdateAsync(id,model);

            return Ok(response.Data);
        }

        [HttpDelete("{id:guid}"), Authorize]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await _eventService.DeleteAsync(id);

            return Ok(response.Data);
        }
    }
}
