using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Domain.Entities
{
    public class ParticipantEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public DateTime DateOfBirth { get; set; } 

        public DateTime DateOfRegistry { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public Guid? EventId { get; set; }

        public EventEntity? Event { get; set; }
    }
}
