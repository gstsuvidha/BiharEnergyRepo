using BiharEnergy.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiharEnergy.Persistence.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        protected RepositoryBase(DbContext context)
        {
            _dbSet = context.Set<T>();
        }
         public abstract Task<T> GetAsync(object id, string accountingUnitId);

        public abstract Task<T> GetAllAsync(object id);
        //        public abstract Task<T> GetAsync(object id);
        // public Task<T> GetAsync(object id)
        // {
        //     return _dbSet.FindAsync(id);
        // }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }
    }

}