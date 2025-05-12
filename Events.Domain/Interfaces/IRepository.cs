using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        public Task<T?> GetAsync(Guid id);

        public IQueryable<T> GetAllAsync();

        public Task AddAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(T entity);
    }
}
