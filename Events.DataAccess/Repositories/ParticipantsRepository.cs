using Events.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.DataAccess;
using Events.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;

namespace Events.DataAccess.Repositories
{
    public class ParticipantsRepository : BaseRepository<ParticipantEntity>, IParticipantsRepository
    {
        public ParticipantsRepository(EventsDbContext context)
            : base(context) { }

        public async Task<ParticipantEntity?> GetAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Participants
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
        }
    }
}
