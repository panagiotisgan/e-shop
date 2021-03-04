using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess
{
    public class ImageUnitOfWork : UnitOfWork, IImageUnitOfWork
    {
        public IImageDbRepository ImageDbRepository { get; private set; }
        public ImageUnitOfWork(IImageDbRepository dbRepository, EshopDbContext dbContext): base(dbContext)
        {
            this.ImageDbRepository = dbRepository;
        }
    }

    public interface IImageUnitOfWork : IUnitOfWork
    {
        IImageDbRepository ImageDbRepository { get; }
    }
}
