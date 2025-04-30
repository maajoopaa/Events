using Events.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Events.DataAccess.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly EventsDbContext _context;

        public EventsRepository(EventsDbContext context)
        {
            _context = context;
        }

        public async Task<EventEntity?> GetAsync(Guid id)
        {
            return await _context.Events
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<EventEntity>> GetAllAsync()
        {
            return await _context.Events
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<EventEntity> AddAsync(EventEntity entity)
        {
            await _context.Events.AddAsync(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<EventEntity> UpdateAsync(EventEntity entity)
        {
            _context.Events.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Guid> DeleteAsync(EventEntity entity)
        {
            _context.Events.Remove(entity);

            await _context.SaveChangesAsync();

            return entity.Id;
        }
    }
}
