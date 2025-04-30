using Events.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Domain.Entities
{
    public class EventEntity
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime HoldedAt { get; set; }

        public string Venue { get; set; } = null!;

        public Category Category { get; set; }

        public int MaxCountOfParticipant { get; set; } = 0;

        public ICollection<ParticipantEntity> Participants { get; set; } = [];
    }
}
