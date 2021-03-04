using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.IRepositories
{
    public interface IUserRepository:IGetByNameRepository<User,EshopDbContext>
    {
        User GetByEmail(string email);
        User GetByPhoneNumber(string phone);
        User GetByAddress(string address);
        User GetUserByCredentialId(long credentialId);
        bool AdminExist();
    }
}
