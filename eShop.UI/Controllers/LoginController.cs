using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eShop.DataAccess;
using eShop.DataAccess.IRepositories;
using eShop.DataAccess.Services;
using eShop.UI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace eShop.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ICredentialRepository _credentialRepo;
        //private readonly IUserRepository _userRepo;
        public LoginController(ICredentialRepository credentialRepo/*,IUserRepository userRepo*/)
        {
            this._credentialRepo = credentialRepo;
            //this._userRepo = userRepo;
        }
        [HttpGet]
        public IActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult UserLogin(string username, string password,bool isHuman=false)
        {
            const string ERROR_MSG = "Invalid Username or password.Try again!";
            string errorMsg = null;
            if (!isHuman)
                throw new ArgumentException(nameof(isHuman));

            var accountExist = _credentialRepo.GetByName(username);
            if (accountExist == null)
            {
                errorMsg = ERROR_MSG;
                ViewBag.Error = errorMsg;
                return View();
            }

            var user = _userRepo.GetUserByCredentialId(accountExist.Id);
            if(!user.IsActiveAccount)
            {
                errorMsg = "You have receive an verification link in your email.Please activate your account.";
                ViewBag.Error = errorMsg;
                return View();
            }

            if (password == null)
            {
                if(errorMsg == null)
                    errorMsg = ERROR_MSG;

                ViewBag.Error = errorMsg;
                return View();
            }
            if (String.IsNullOrWhiteSpace(password))
            {
                if (errorMsg == null)
                    errorMsg = ERROR_MSG;

                ViewBag.Error = errorMsg;
                return View();
            }

            var hash = PasswordGenerator.Hash(password, Convert.FromBase64String(accountExist.Salt));

            var realPassword = Convert.ToBase64String(hash);

            if (realPassword != accountExist.Password)
            {
                if(errorMsg==null)
                {
                    errorMsg = ERROR_MSG;
                }
                ViewBag.Error = errorMsg;
                return View();
            }


            return View();
        }
    }
}