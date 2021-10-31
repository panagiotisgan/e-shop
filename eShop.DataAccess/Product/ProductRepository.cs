using eShop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess
{
    public class ProductRepository : GenericRepository<Product, EshopDbContext>, IProductDbRepository
    {
        public ProductRepository(EshopDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var result =  await _context.Products.Include(x =>  x.Category).Include(y=>y.Images).ToListAsync();
            return result;
        }
    }

    public interface IProductDbRepository : IDbRepository<Product>, IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
    }

}
