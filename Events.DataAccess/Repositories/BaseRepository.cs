using Events.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Events.DataAccess.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly EventsDbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(EventsDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression, int page, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .AsNoTracking()
                .Where(expression)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task<T?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbSet
                .FindAsync(id, cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
