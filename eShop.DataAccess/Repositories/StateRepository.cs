using eShop.DataAccess.IRepositories;
using eShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eShop.DataAccess.Repositories
{
    public class StateRepository : GenericRepository<State, EshopDbContext>, IStateRepository
    {
        
        public StateRepository(EshopDbContext dbContext):base(dbContext)
        {
                
        }

        public IEnumerable<State> GetStateByCountryId(long countryId)
        {
             return this._context.States.Where(s => s.CountryId == countryId).OrderBy(s=>s.Name).AsEnumerable();
        }
    }
}
