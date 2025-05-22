namespace Events.Domain.Models
{
    public class Event
    {
        public Event(Guid id, string title, string description, DateTime holdedAt, string venue, Category category,
            int maxCountOfParticipant, List<Participant> participants, string? image)
        {
            Id = id;
            Title = title;
            Description = description;
            HoldedAt = holdedAt;
            Venue = venue;
            Category = category;
            MaxCountOfParticipant = maxCountOfParticipant;
            Participants = participants;
            ImageUrl = image;
        }

        public Guid Id { get; }

        public string Title { get; }

        public string Description { get; }

        public DateTime HoldedAt { get; }

        public string Venue { get; }

        public Category Category { get; }

        public int MaxCountOfParticipant { get; }

        public List<Participant> Participants { get; }

        public string? ImageUrl { get; }
    }
}
