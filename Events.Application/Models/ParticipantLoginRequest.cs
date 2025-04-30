namespace Events.Application.Models
{
    public class ParticipantLoginRequest
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
