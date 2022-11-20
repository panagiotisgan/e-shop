using eShop.DataAccess.IRepositories;
using eShop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess
{
    public class GenericRepository<TEntity, TContext> : IDbRepository<TEntity>
        where TEntity : BaseEntity
        where TContext : EshopDbContext
    {
        protected readonly TContext _context;
        public GenericRepository(TContext context)
        {
            this._context = context;
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity GetById(long modelId)
        {
            return _context.Set<TEntity>().Find(modelId);
        }
        //Update Entire entity
        public virtual void UpdateEntity(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void CreateEntity(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
        }

        public void DeleteEntity(long entityId)
        {
            var entity = GetById(entityId);
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this._context.Set<TEntity>().ToListAsync();
        }

        //Να φύγει μόλις τα γυρίσω όλα σε UnitOfWork
        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<TEntity> GetByIdAsync(long entityId)
        {
            return await _context.Set<TEntity>().FindAsync(entityId);
        }

        public IQueryable<TEntity> GetQueryable()
        {
            return _context.Set<TEntity>().AsQueryable();
        }        
    }
}
