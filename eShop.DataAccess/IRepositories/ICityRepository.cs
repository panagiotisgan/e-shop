using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.IRepositories
{
    public interface ICityRepository:IBaseRepository<City>
    {
        IEnumerable<City> GetCitiesByStateId(long stateId);
    }
}
