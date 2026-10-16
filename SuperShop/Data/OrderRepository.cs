using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;
using SuperShop.Helpers;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {

        private readonly DataContext _context;

        private readonly IUserHelper _userHelper;
        public OrderRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task<IQueryable<Order>> GetOrderAsync(string username)
        {
            var user = await _userHelper.GetUserByEmailAsync(username);
            // if user is null, return empty list
            if (user == null)
            {
                return null;
            }
            // if user admin, return all orders
            // if user is not admin, return only orders of that user

            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                // return all orders by order date descending
                // "items" are all the order details (individual orders)
                return _context.Orders
                    .Include(o => o.Items)
                    .ThenInclude(p => p.Product)
                    .OrderByDescending(o => o.OrderDate);
            }
            // if not admin, return only user's orders by order date descending
            return _context.Orders
                .Include(o => o.Items)
                .ThenInclude(p => p.Product)
                .Where(o => o.User == user)
                .OrderByDescending(o => o.OrderDate);
        }
    }
}
