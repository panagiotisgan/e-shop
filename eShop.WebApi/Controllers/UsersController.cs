using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShop.DataAccess;
using eShop.DataAccess.AdditionalDetailsModels;
using eShop.DataAccess.DTOs;
using eShop.DataAccess.IRepositories;
using eShop.DataAccess.IServices;
using eShop.Model;
using eShop.WebApi.Filters;
using eShop.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eShop.WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IUserUnitOfWork _userUnitOfWork;
       //public HttpContext _httpContext;
        public UsersController(IUserUnitOfWork _userUnitOfWork, ILoginService loginService/*, HttpContext httpContext*/)
        {
            //this._loginService = loginService;
            this._userUnitOfWork = _userUnitOfWork;
            this._loginService = loginService;
            //this._httpContext = httpContext;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        [AuthenticationFilter]
        public IActionResult Authenticate([FromBody]AuthenticateCredentials authenticateModel)
        {
            var isAuthenticate = _loginService.PasswordSignIn(authenticateModel.UserName, authenticateModel.Password, authenticateModel.IsHuman);

            AuthenticationResultDTO authenticatedUserResult = new AuthenticationResultDTO();

            if (!isAuthenticate.accountErrors.IsValid && isAuthenticate.accountErrors.ErrorMessage.Contains("You have receive an verification link in your email.Please activate your account."))
            {
                authenticatedUserResult.IsAuthenticated = false;
                authenticatedUserResult.ErrorMessage = "You have receive an verification link in your email.Please activate your account.";
                return BadRequest(authenticatedUserResult);
            }


            if (!isAuthenticate.accountErrors.IsValid && !isAuthenticate.accountErrors.ErrorMessage.Contains("You have receive an verification link in your email.Please activate your account."))
            {
                authenticatedUserResult.ErrorMessage = "Username or Password is incorrect";
                authenticatedUserResult.IsAuthenticated = false;
                return BadRequest(authenticatedUserResult);
            }
                       
            authenticatedUserResult.UserId = isAuthenticate.user.UserId;
            authenticatedUserResult.Token = isAuthenticate.user.Token;
            authenticatedUserResult.Role = isAuthenticate.user.Role;
            authenticatedUserResult.IsAuthenticated = true;
            authenticatedUserResult.ErrorMessage = null;

            //_httpContext.Session.SetString("JWToken", isAuthenticate.user.Token);

            return Ok(authenticatedUserResult);
        }

        [AllowAnonymous]
        [HttpPost("AccountCreate")]
        [CreateAccountFilter] //Otan exw to filter den ftanei to request
        public IActionResult CreateUser([FromBody] UserDTO user)
        {
            return null;
        }

        //api/Users
        [HttpGet]
        //[Authorize(Roles = Role.User)]
        public IActionResult GetAll()
        {
            var users = _userUnitOfWork.UserDbRepository.GetAll();
            //this._context.User.Identity.

            return Ok(users);
        }
    }
}