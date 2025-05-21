using Events.Domain.Entities;
using Events.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Events.DataAccess.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly EventsDbContext _context;

        public EventsRepository(EventsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EventEntity entity, CancellationToken cancellationToken = default)
        {
            await _context.Events.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(EventEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Events.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<EventEntity>> GetAllAsync(Expression<Func<EventEntity, bool>> expression, int page, int pageSize,
            CancellationToken cancellationToken = default)
        {
            var result = await _context.Events
                .Where(expression)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return result;

        }

        public async Task<EventEntity?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Events
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task UpdateAsync(EventEntity entity, CancellationToken cancellationToken = default)
        {
            _context.Events.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
