using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    // standard is "T" for generic type; accepts any class
    public interface IGenericRepository<T> where T : class
    {
        // method that returns all entities of type T (could be products, clients, etc)
        IQueryable<T> GetAll();

        Task<T> GetByIdAsync(int id);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<bool> ExistsAsync(int id);



    }
}
