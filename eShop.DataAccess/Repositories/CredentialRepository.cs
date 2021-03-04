using eShop.DataAccess.IRepositories;
using eShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eShop.DataAccess.Repositories
{
    public class CredentialRepository : GenericRepository<Credential,EshopDbContext>,ICredentialRepository
    {
        public CredentialRepository(EshopDbContext context) : base(context)
        {

        }

        //Προσωρινα μετα τα Τεστ θα φύγει
        //EshopDbContext _context = new EshopDbContext();

        public Credential GetByName(string name)
        {
            return this._context.Credentials
                .Where(c => c.Username == name)
                .FirstOrDefault();
        }

        public bool NameExist(string name)
        {
            return this._context.Credentials.Any(c => c.Username.Equals(name));
        }
    }
}
