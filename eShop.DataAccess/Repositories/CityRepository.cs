using eShop.DataAccess.IRepositories;
using eShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eShop.DataAccess.Repositories
{
    public class CityRepository : GenericRepository<City, EshopDbContext>, ICityRepository
    {
        public CityRepository(EshopDbContext dbContext):base(dbContext)
        {

        }
        public IEnumerable<City> GetCitiesByStateId(long stateId)
        {
            return this._context.Cities.Where(c => c.StateId == stateId).ToList().OrderBy(c=>c.Name);
        }
    }
}
