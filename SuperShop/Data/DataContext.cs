using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;

namespace SuperShop.Data
{
    //DataContext: classe especifica que é responsável pela ligação à base de dados
    // o nome "DataContext" é um standard
    // DataContext : DbContext - meu datacontext herda da classe DbContext do EntityFrameworkCore
    public class DataContext : DbContext
    {
        // cria uma tabela
        // propriedade que vai ficar ligada a tabela products atraves do DataContext
        public DbSet<Product> Products { get; set; }

        // injeto a minha classe DataContext e uso herança de DbContext para poder usar o EntityFrameworkCore
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
    }
}
