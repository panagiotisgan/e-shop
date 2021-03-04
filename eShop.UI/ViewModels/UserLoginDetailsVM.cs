using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.UI.ViewModels
{
    public class UserLoginDetailsVM
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsHuman { get; private set; }


        //public void SetUsername(string username)
        //{
        //    if (username == null)
        //        throw new ArgumentNullException(nameof(username));
        //    if (String.IsNullOrWhiteSpace(username))
        //        throw new Exception("The value of username cannot be null");

        //    this.Username = username;
        //}

        //public void SetPassword(string password)
        //{
        //    if (password == null)
        //        throw new ArgumentNullException(nameof(password));
        //    if (String.IsNullOrWhiteSpace(password))
        //        throw new Exception("The value of username cannot be null");

        //    this.Password = password;
        //}
    }
}
