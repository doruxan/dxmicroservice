using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPLOGMicroservice.Data.Core
{
    public interface IRepository<T> where T : IEntity
    {
        IQueryable<T> GetQueryable();

        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(object id);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task DeleteAsync(object id);

    }
}
