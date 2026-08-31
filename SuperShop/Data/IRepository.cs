
using System.Collections.Generic;
using System.Threading.Tasks;
using SuperShop.Data.Entities;


namespace SuperShop.Data
{
    // interface auto-created by right clicking "Repository" -> Quick Actions and Refactorings > Extract Interface > IRespository
    // could have done interface first, but this way is easier to create the interface with all the methods already in place
    public interface IRepository
    {
        void AddProduct(Product product);

        Product GetProduct(int id);

        IEnumerable<Product> GetProducts();

        bool ProductExists(int id);

        void RemoveProduct(Product product);

        Task<bool> SaveAllAsync();

        void UpdateProduct(Product product);
    }
}