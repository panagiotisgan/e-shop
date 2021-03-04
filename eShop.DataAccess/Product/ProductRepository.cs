using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess
{
    public class ProductRepository : GenericRepository<Product, EshopDbContext>, IProductDbRepository
    {
        public ProductRepository(EshopDbContext context) : base(context)
        {

        }
    }

    public interface IProductDbRepository : IDbRepository<Product>, IProductRepository
    {

    }

}
