using System.Collections.Generic;
using System.Threading.Tasks;

namespace BiharEnergy.Core.Domain
{
    public interface IRepositoryBase<T> where T : class
    {
         Task<T> GetAsync(object id,string accountingUnitId);

        Task<T> GetAllAsync(object id);  //TO DO Logic Check

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);
    }
}