using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.UI.ViewModels
{
    public class LoginDetailsVM
    {
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsHuman { get; private set; }
    }
}
