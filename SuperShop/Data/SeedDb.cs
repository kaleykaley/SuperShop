using Microsoft.AspNetCore.Identity;
using SuperShop.Data.Entities;
using SuperShop.Helpers;
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

        //private readonly UserManager<User> _userManager;

        private readonly IUserHelper _userHelper;
        // to randomly generate the products
        private Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        // vai criar o seed de forma asincrona, para nao bloquear a thread principal
        public async Task SeedAsync()
        {
            // first, ver se a DB já está criada e se não
            // estiver, cria a DB
            await _context.Database.EnsureCreatedAsync();

            var user = await _userHelper.GetUserByEmailAsync("rafaasfs@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Rafael",
                    LastName = "Santos",
                    Email = "rafaasfs@gmail.com",
                    UserName = "rafaasfs@gmail.com",
                    PhoneNumber = "21234555"
                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }
            }



            // if not products, create the method to create the products
            if (!_context.Products.Any() )
            {
                AddProduct("IPhone X", user);
                AddProduct("Magic Mouse", user);
                AddProduct("iWatch Series 4", user);
                AddProduct("iPad Mini", user);
                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            _context.Products.Add(new Product
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvailable = true,
                Stock = _random.Next(100),
                User = user
            });
        }
    }
}
