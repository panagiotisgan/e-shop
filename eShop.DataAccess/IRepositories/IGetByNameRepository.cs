using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.IRepositories
{
    /// <summary>
    /// This repo implemented for entities which contains name and username as property 
    /// </summary>
    public interface IGetByNameRepository<T,TContext> :IBaseRepository<T> 
        where T :BaseEntity
        where TContext : EshopDbContext
    {
        T GetByName(string name);
        bool NameExist(string name);
    }
}
