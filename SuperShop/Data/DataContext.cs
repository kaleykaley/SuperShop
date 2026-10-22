using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;
using System.Linq;

namespace SuperShop.Data
{
    //DataContext: classe especifica que é responsável pela ligação à base de dados
    // o nome "DataContext" é um standard
    // DataContext : DbContext - meu datacontext herda da classe DbContext do EntityFrameworkCore
    public class DataContext : IdentityDbContext<User>
    {
        // cria uma tabela
        // propriedade que vai ficar ligada a tabela products atraves do DataContext
        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<OrderDetailTemp> OrderDetailTemps { get; set; }

        // injeto a minha classe DataContext e uso herança de DbContext para poder usar o EntityFrameworkCore
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }


        // Habilitar a regra de apagar em cascata (cascade delete rule)
        // override do método em que eu crio o modelo
        // to not allow deleting of orders with products
        // (of foreign keys with ownership)
        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach(var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }*/
    }
}
