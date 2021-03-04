using eShop.DataAccess.IRepositories;
using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.Repositories
{
    public class CountryRepository : GenericRepository<Country,EshopDbContext>,ICountryRepository
    {
        //private readonly EshopDbContext context = new EshopDbContext();
        public CountryRepository(EshopDbContext context):base(context)
        {

        }
    }
}
