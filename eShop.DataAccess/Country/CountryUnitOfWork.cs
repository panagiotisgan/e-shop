using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess
{
    public class CountryUnitOfWork : UnitOfWork, ICountryUnitOfWork
    {
        public ICountryDbRepository CountryDbRepository { get; private set; }
        public CountryUnitOfWork(ICountryDbRepository countryDbRepository, EshopDbContext context): base(context)
        {
            CountryDbRepository = countryDbRepository;
        }
    }

    public interface ICountryUnitOfWork : IUnitOfWork
    {
        ICountryDbRepository CountryDbRepository { get; }
    }
}
