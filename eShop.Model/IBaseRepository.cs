using eShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Model
{
    public interface IBaseRepository<TEntity> 
        where TEntity:BaseEntity
    {
        IEnumerable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        TEntity GetById(long entityId);
        Task<TEntity> GetByIdAsync(long entityId);
        void CreateEntity(TEntity entity);
        void DeleteEntity(long entityId);
        void UpdateEntity(TEntity entity);
        IQueryable<TEntity> GetQueryable();
        int Save();
    }
}
