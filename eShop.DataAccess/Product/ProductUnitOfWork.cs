using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess
{
    public class ProductUnitOfWork : UnitOfWork, IProductUnitOfWork
    {
        public IProductDbRepository ProductRepository { get; private set; }
        public ProductUnitOfWork(EshopDbContext dbContext,IProductDbRepository productRepository) : base(dbContext)
        {
            this.ProductRepository = productRepository;
        }        
    }

    public interface IProductUnitOfWork : IUnitOfWork
    {
        IProductDbRepository ProductRepository { get; }
    }
}
