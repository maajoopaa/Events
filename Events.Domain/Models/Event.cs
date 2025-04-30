using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Domain.Models
{
    public class Event
    {
        public Guid Id { get; }

        public string Title { get; }

        public string Description { get; }

        public DateTime HoldedAt { get; }

        public string Venue { get; }

        public Category Category { get; }

        public int MaxCountOfParticipant { get; }

        public List<Participant> Participants { get; }
    }
}
