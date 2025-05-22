using Events.Domain.Entities;
using Events.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Events.Application.Models
{
    public class EventRequest
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime HoldedAt { get; set; }

        public string Venue { get; set; } = null!;

        public Category Category { get; set; }

        public int MaxCountOfParticipant { get; set; } = 0;

        public IFormFile Image { get; set; }
    }
}
