using SuperShop.Data.Entities;

namespace SuperShop.Data
{
    /// <summary>
    /// DELETED?
    /// </summary>
    
    public class ProductRepository : GenericRepository<Product>, IProductRepository 
    {

        public ProductRepository(DataContext context) : base(context)
        {
            
        }
    } 
}
