using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess
{
    public class CategoryUnitOfWork : UnitOfWork, ICategoryUnitOfWork
    {
        public ICategoryDbRepository CategoryDbRepository { get; private set; }
        public CategoryUnitOfWork(ICategoryDbRepository categoryDbRepository, EshopDbContext dbContext) : base(dbContext)
        {
            this.CategoryDbRepository = categoryDbRepository;
        }
    }

    public interface ICategoryUnitOfWork : IUnitOfWork
    {
        ICategoryDbRepository CategoryDbRepository { get; }
    }
}
