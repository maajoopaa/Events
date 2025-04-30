using Events.Domain.Entities;
using Events.Domain.Models;

namespace Events.Application.Models
{
    public class EventRequest
    {
        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string Venue { get; set; } = null!;

        public Category Category { get; set; }

        public int MaxCountOfParticipant { get; set; } = 0;
    }
}
