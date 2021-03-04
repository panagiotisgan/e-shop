using eShop.DataAccess.IRepositories;
using eShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.UI.UIServices
{
    public class CountryServices
    {
        private readonly ICountryRepository _countryRepository;
        public CountryServices(ICountryRepository countryRepository)
        {
            this._countryRepository = countryRepository;
        }

        public IEnumerable<Country> GetCountries()
        {
            return _countryRepository.GetAll().OrderBy(c=>c.Name);
        }
    }
}
