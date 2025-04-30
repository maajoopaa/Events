using Events.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Application.Models
{
    public class AccountResponse
    {
        public string? Token { get; set; }

        public Participant? Account { get; set; }
    }
}
