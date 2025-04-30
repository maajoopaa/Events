using Events.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.DataAccess;

namespace Events.DataAccess.Repositories
{
    public class ParticipantsRepository : IParticipantsRepository
    {
        private readonly EventsDbContext _context;

        public ParticipantsRepository(EventsDbContext context)
        {
            _context = context;
        }

        public async Task<ParticipantEntity?> GetAsync(Guid id)
        {
            return await _context.Participants
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<ParticipantEntity>> GetAllAsync()
        {
            return await _context.Participants
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ParticipantEntity> AddAsync(ParticipantEntity entity)
        {
            await _context.Participants.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<ParticipantEntity> UpdateAsync(ParticipantEntity entity)
        {
            _context.Participants.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Guid> DeleteAsync(ParticipantEntity entity)
        {
            _context.Participants.Remove(entity);

            await _context.SaveChangesAsync();

            return entity.Id;
        }
    }
}
