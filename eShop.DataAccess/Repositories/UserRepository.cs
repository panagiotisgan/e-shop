using eShop.DataAccess.IRepositories;
using eShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eShop.DataAccess.Repositories
{
    public class UserRepository : GenericRepository<User, EshopDbContext>, IUserRepository
    {

        //List<User> UsersPool = new List<User>()
        //{
        //    new User
        //    {
        //        AddressNo1 = "Lapithwn 10",
        //        AddressNo2 = null,
        //        Country = new Country(),
        //        Email = "panos@hmt.xom",
        //        IsActiveAccount = true,
        //        FirstName ="Panos",
        //        LastName = "Gkan",
        //        Credential = new Credential()
        //        {
        //            Password = "panikas88",
        //            Salt = "sdfjkbsdfh",
        //            Username = "panagiotis88"
        //        },
        //        PhoneNumber = "690909090",
        //        Role = Role.User1,
        //        VATNumber = "10558040"
        //    },
        //     new User
        //    {
        //        AddressNo1 = "Nikis 10",
        //        AddressNo2 = null,
        //        Country = new Country(),
        //        Email = "xxxxs@hmt.xom",
        //        IsActiveAccount = true,
        //        FirstName ="Andrew",
        //        LastName = "Akratovski",
        //        Credential = new Credential()
        //        {
        //            Password = "lakisLakis",
        //            Salt = "sdfjkbsdfh",
        //            Username = "thrashRevolver"
        //        },
        //        PhoneNumber = "690909100",
        //        Role = Role.User1,
        //        VATNumber = "80558400"
        //    },
        //      new User
        //    {
        //        AddressNo1 = "Aulidos 30",
        //        AddressNo2 = null,
        //        Country = new Country(),
        //        Email = "autos_that@hot.com",
        //        IsActiveAccount = true,
        //        FirstName ="Nikos",
        //        LastName = "Gkour",
        //        Credential = new Credential()
        //        {
        //            Password = "panikas88",
        //            Salt = "sdfjkbsdfh",
        //            Username = "nikolas"
        //        },
        //        PhoneNumber = "690909333",
        //        Role = Role.User1,
        //        VATNumber = "10558040"
        //    }

        //};


        public UserRepository(EshopDbContext context) : base(context)
        {
        }

        public User GetByAddress(string address)
        {
            return this._context.Users
                .Where(x => x.AddressNo1.Equals(address))
                .FirstOrDefault();
        }

        public User GetUserByCredentialId(long credentialId)
        {
            return this._context.Users
                .Where(u => u.CredentialId == credentialId)
                .FirstOrDefault();
        }

        public User GetByEmail(string email)
        {
            return this._context.Users
                .Where(x => x.Email.Equals(email))
                .FirstOrDefault();
        }

        public User GetByName(string name)
        {
            return this._context.Users
                .Where(x => (x.FirstName + " " + x.LastName).Equals(name))
                .FirstOrDefault();
        }

        public User GetByPhoneNumber(string phone)
        {
            return this._context.Users
                .Where(x => x.PhoneNumber.Equals(phone))
                .FirstOrDefault();
        }

        /// <summary>
        /// name parame here represent the username of a User
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool NameExist(string name)
        {
            return _context.Users.Any(x => x.Credential.Username.Equals(name));
        }

        public bool AdminExist()
        {
            return _context.Users.Any(u => u.Role == Role.Admin);
        }
    }
}
