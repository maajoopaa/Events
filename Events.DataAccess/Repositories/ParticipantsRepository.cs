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

namespace Events.DataAccess.Repositories
{
    public class ParticipantsRepository : IParticipantsRepository
    {
        private readonly EventsDbContext _context;

        public ParticipantsRepository(EventsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ParticipantEntity entity, CancellationToken cancellationToken = default)
        {
            await _context.Participants.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(ParticipantEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Participants.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public IQueryable<ParticipantEntity> GetAll()
        {
            return _context.Participants
                .AsNoTracking();
        }

        public async Task<ParticipantEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Participants
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(ParticipantEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Participants.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
