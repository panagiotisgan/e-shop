using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.DataAccess;
using eShop.DataAccess.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace eShop.UI.Controllers
{    
    public class AccountController : Controller
    {
        private readonly LoginService _loginService;
        private readonly ICredentialRepository _credentialRepo;
        private readonly IUserRepository _userRepo;
        public AccountController(ICredentialRepository credentialRepo,IUserRepository userRepo)
        {
            this._credentialRepo = credentialRepo;
            this._userRepo = userRepo;
            this._loginService = new LoginService(_userRepo, _credentialRepo);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username,string password, bool isHuman)
        {
            if (ModelState.IsValid)
            {
                 var result = this._loginService.PasswordSignIn(username, password, isHuman);

                if (result.accountErrors.IsValid && result.user.Role == "User")
                {
                    return RedirectToAction("Home", "DashboardAdmin");
                }
            }

            return View();
        }
    }
}