using eShop.DataAccess;
using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess
{
    public interface IDbRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity 
    {
    }
}
