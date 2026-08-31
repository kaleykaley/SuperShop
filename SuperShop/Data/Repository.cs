using System.Linq;
using System.Collections.Generic;
using SuperShop.Data.Entities;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    // **** DELETED BY PROFESSOR ***

    // classe que vai aceder a datacontext e fazer CRUD

    // all of these methods are from entity framework (add, update, find, etc)

    // added interface by right clicking "Repository" -> Quick Actions and Refactorings > Extract Interface > IRespository
    /*public class Repository : IRepository
    {
        private readonly DataContext _context;
        public Repository(DataContext context)
        {
            _context = context;

        }
        // ---CRUD METHODS---

        // ienumerable - que me traz as coisas da tabela
        public IEnumerable<Product> GetProducts()
        {
            // give me all the products ordered by name
            return _context.Products.OrderBy(p => p.Name);
        }
        // methods Find/Add/Update/Remove from Entity Framework
        public Product GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
        }

        public void RemoveProduct(Product product)
        {
            _context.Products.Remove(product);
        }

        // ---HELPER METHODS---
        public async Task<bool> SaveAllAsync()
        {
            // grava tudo que esta pendente para a base de dados
            // retorna true se for maior que 0 (se gravou alguma coisa)
            return (await _context.SaveChangesAsync()) > 0;
        }

        // checks if there are any products with given id
        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }
    } */
}
