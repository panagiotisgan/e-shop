using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EshopDbContext _context;
        public UnitOfWork(EshopDbContext context)
        {
            this._context = context;
        }

        public bool IsChangeTrackerEnabled => throw new NotImplementedException();

        public void DisableChangeTracker()
        {
            throw new NotImplementedException();
        }

        public void EnableChangeTracker()
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            this._context.SaveChanges();
        }
    }

    public interface IUnitOfWork
    {
        void Save();
        void DisableChangeTracker();

        void EnableChangeTracker();

        bool IsChangeTrackerEnabled { get; }
    }
}
