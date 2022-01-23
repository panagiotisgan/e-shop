﻿using eShop.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<User> GetUserByCredentialId(long id)
        {
            return await this._context.Users.FirstOrDefaultAsync(x=>x.CredentialId == id);
        }
    }

    public interface IUserDbRepository : IDbRepository<eShop.Model.User>, IUserRepository
    {
        bool AdminExist();
        Task<User> GetUserByCredentialId(long id);
    }
}
