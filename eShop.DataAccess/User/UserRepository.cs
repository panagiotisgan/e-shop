using eShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eShop.DataAccess
{
    public class UserRepository : GenericRepository<eShop.Model.User, EshopDbContext>, IUserDbRepository
    {
        public UserRepository(EshopDbContext context) : base(context)
        {
        }

        public bool AdminExist()
        {
            return this._context.Users.Any(x => x.Role == Role.Admin);
        }

        public User GetUserByCredentialId(long id)
        {
            return this._context.Users.FirstOrDefault(x=>x.CredentialId == id);
        }
    }

    public interface IUserDbRepository : IDbRepository<eShop.Model.User>, IUserRepository
    {
        bool AdminExist();
        User GetUserByCredentialId(long id);
    }
}
