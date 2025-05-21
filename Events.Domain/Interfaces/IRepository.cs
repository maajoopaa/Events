using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Events.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<T?> GetAsync(Guid id, CancellationToken cancellationToken = default);

        public Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression,int page, int pageSize, 
            CancellationToken cancellationToken = default);

        public Task AddAsync(T entity, CancellationToken cancellationToken = default);

        public Task UpdateAsync(T entity, CancellationToken cancellationToken = default);

        public Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    }
}
