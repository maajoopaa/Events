using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Domain.Models
{
    public class Event
    {
        public Event(Guid id, string title, string description, DateTime holdedAt, string venue, Category category,
            int maxCountOfParticipant, List<Participant> participants)
        {
            Id = id;
            Title = title;
            Description = description;
            HoldedAt = holdedAt;
            Venue = venue;
            Category = category;
            MaxCountOfParticipant = maxCountOfParticipant;
            Participants = participants;
        }

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
