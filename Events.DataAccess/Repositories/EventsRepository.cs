using Events.Domain.Entities;
using Events.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Events.DataAccess.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly EventsDbContext _context;

        public EventsRepository(EventsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EventEntity entity)
        {
            await _context.Events.AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EventEntity entity)
        {
            _context.Events.Remove(entity);

            await _context.SaveChangesAsync();
        }

        public IQueryable<EventEntity> GetAllAsync()
        {
            return _context.Events
                .AsNoTracking();
        }

        public async Task<EventEntity?> GetAsync(Guid id)
        {
            return await _context.Events
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(EventEntity entity)
        {
            _context.Events.Remove(entity);

            await _context.SaveChangesAsync();
        }
    }
}
