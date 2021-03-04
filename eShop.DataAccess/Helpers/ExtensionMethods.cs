using eShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.DataAccess.Helpers
{
    public static class ExtensionMethods
    {
        public static IEnumerable<User> GetUsers(this IEnumerable<User> users)
        {
            if (users.Count() == 0)
                return null;

            return users.Select(x => x.WithoutPassword());
        }

        public static User WithoutPassword(this User user)
        {
            if (user == null)
                return null;

            user.Credential.Password = null;
            user.Credential.Salt = null;
            return user;
        }
    }
}
