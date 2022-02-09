using eShop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess
{
    public class StateRepository : GenericRepository<State, EshopDbContext> , IStateDbRepository
    {
        
        public StateRepository(EshopDbContext context) : base(context)
        {
        }

        public async Task<State> GetByNameAsync(string stateName)
        {
            return await _context.States.FirstOrDefaultAsync(x=>x.Name == stateName);
        }

        public IEnumerable<State> GetStateByCountryId(long countryId)
        {
            throw new NotImplementedException();
        }
    }

    public interface IStateDbRepository : IDbRepository<State>, IStateRepository
    {
        IEnumerable<State> GetStateByCountryId(long countryId);
        Task<State> GetByNameAsync(string stateName);
    }
}
