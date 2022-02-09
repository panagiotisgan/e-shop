using eShop.DataAccess.IRepositories;
using eShop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess
{
    public class CountryRepository : GenericRepository<Country, EshopDbContext>, ICountryDbRepository 
    { 
        public CountryRepository(EshopDbContext context):base(context)
        {

        }
    }

    public interface ICountryDbRepository : IDbRepository<Country>, ICountryRepository 
    { 
    }
}
