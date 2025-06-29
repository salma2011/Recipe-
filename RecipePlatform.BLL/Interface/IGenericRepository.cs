using System.Collections.Generic;
using System.Threading.Tasks;
using RecipePlatform.Models;

namespace RecipePlatform.BLL.Iterface
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();

        T GetById(int id);
        Task<T> GetByIdAsync(int id);

        void Add(T entity);
        Task AddAsync(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}
