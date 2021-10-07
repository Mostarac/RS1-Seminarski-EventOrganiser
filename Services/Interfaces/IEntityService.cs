using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapp.Services.Interfaces
{
    public interface IEntityService<T> where T : class 
    {
        void Create(T entity);

        Task CreateAsync(T entity);

        void Update(T entity);

        Task UpdateAsync(T entity);

        void Delete(T entity);

        Task DeleteAsync(T entity);
       
        IQueryable<T> GetAll();

        Task<List<T>> GetAllAsync();

        T GetById(object id);

        Task<T> GetByIdAsync(object id);
    }
}
