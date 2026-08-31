using Microsoft.EntityFrameworkCore;
using SuperShop.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SuperShop.Data
{
    public interface IProductRepository : IGenericRepository<Product>
    {


    }
}
