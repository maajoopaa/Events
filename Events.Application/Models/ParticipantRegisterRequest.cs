namespace Events.Application.Models
{
    public class ParticipantRegisterRequest
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
