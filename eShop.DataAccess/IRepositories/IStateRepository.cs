using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.IRepositories
{
    public interface IStateRepository : IBaseRepository<State>
    {
        IEnumerable<State> GetStateByCountryId(long countryId);
    }
}
