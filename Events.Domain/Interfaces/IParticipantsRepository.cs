using Events.Domain.Entities;
using Events.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Domain.Interfaces
{
    public interface IParticipantsRepository : IRepository<ParticipantEntity> { }
}
