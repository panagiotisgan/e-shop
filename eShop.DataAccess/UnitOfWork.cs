using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EshopDbContext _context;
        private IDbContextTransaction _dbContextTransaction;
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

        public void StartTransaction()
        {
            _dbContextTransaction = this._context.Database.BeginTransaction();
        }

        public void Rollback()
        {
            _dbContextTransaction.Rollback();
        }

        public void Commit()
        {
            _dbContextTransaction?.Commit();
        }
    }

    public interface IUnitOfWork
    {
        void Save();
        void DisableChangeTracker();

        void EnableChangeTracker();

        bool IsChangeTrackerEnabled { get; }
        void Commit();
        void StartTransaction();
        void Rollback();
    }
}
