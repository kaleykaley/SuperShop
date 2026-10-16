using SuperShop.Data.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        // give me all the orders of a specific user
        Task<IQueryable<Order>> GetOrderAsync(string username);
    }
}
