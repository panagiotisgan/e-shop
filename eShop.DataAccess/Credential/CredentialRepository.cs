using eShop.DataAccess.IRepositories;
using eShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eShop.DataAccess
{
    public class CredentialRepository : GenericRepository<Credential,EshopDbContext>, ICredentialDbRepository
    {
        public CredentialRepository(EshopDbContext context) : base(context)
        {

        }

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

    public interface ICredentialDbRepository : IDbRepository<Credential>, ICredentialRepository
    {
        Credential GetByName(string name);
        bool NameExist(string name);
    }
}
