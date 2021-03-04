using eShop.DataAccess.IRepositories;
using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.Repositories
{
    public class ProductRepository : GenericRepository<Product,EshopDbContext>, IProductRepository
    {
        public ProductRepository(EshopDbContext eshopDbContext): base(eshopDbContext)
        {

        }
    }
}
