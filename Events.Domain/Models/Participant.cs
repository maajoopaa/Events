using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Events.Domain.Models
{
    public class Participant
    {
        public Participant(Guid id, string firstName, string lastName, DateTime dateOfBirth, DateTime dateOfRegistry, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            DateOfRegistry = dateOfRegistry;
            Email = email;
        }

        public Guid Id { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public DateTime DateOfBirth { get; }

        public DateTime DateOfRegistry { get; }

        public string Email { get; }
    }
}
