using eShop.DataAccess.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess
{
    public class PasswordService
    {
        public static (string password,string salt) CreatePassword(string password)
        {
            if (String.IsNullOrWhiteSpace(password) || password.Length < 7)
                throw new ArgumentException("Password cannot be null or less than 7 characters");

            var salt = PasswordGenerator.GetSalt();
            var hash = PasswordGenerator.Hash(password, salt);

            string Passrword = Convert.ToBase64String(hash);
            string Salt = Convert.ToBase64String(salt);

            return (Passrword, Salt);
        }
    }
}
