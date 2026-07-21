using SuperShop.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    // classe que vai popular a base de dados com dados iniciais if they dont exist yet

    // to test, use drop-database in package manager console, then run the app and check if the products are there
    public class SeedDb
    {
        private readonly DataContext _context;

        // to randomly generate the products
        private Random _random;

        public SeedDb(DataContext context)
        {
            _context = context;
            _random = new Random();
        }

        // vai criar o seed de forma asincrona, para nao bloquear a thread principal
        public async Task SeedAsync()
        {
            // first, ver se a DB já está criada e se não
            // estiver, cria a DB
            await _context.Database.EnsureCreatedAsync();

            // if not products, create the method to create the products
            if(!_context.Products.Any() )
            {
                AddProduct("IPhone X");
                AddProduct("Magic Mouse");
                AddProduct("iWatch Series 4");
                AddProduct("iPad Mini");
                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name)
        {
            _context.Products.Add(new Product
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvailable = true,
                Stock = _random.Next(100)
            });
        }
    }
}
