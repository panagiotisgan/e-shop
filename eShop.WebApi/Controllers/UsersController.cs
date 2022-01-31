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
using eShop.WebApi.Auth;
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
        private readonly ICustomManager _jwtTokenManager;
        public UsersController(IUserUnitOfWork _userUnitOfWork, ILoginService loginService, ICustomManager jwtTokenManager)
        {
            this._userUnitOfWork = _userUnitOfWork;
            this._loginService = loginService;
            this._jwtTokenManager = jwtTokenManager;
        }        

        [AllowAnonymous]
        [HttpPost("authentication")]
        public async Task<IActionResult> Authentication([FromForm] AuthenticateCredentials authenticateModel)
        {
            var isCorrect = await this._loginService.CheckCredentials(authenticateModel.UserName, authenticateModel.Password, authenticateModel.IsHuman);
            string jwtToken = null;
            if (isCorrect)
                jwtToken = await _jwtTokenManager.CreateToken(authenticateModel.UserName);

            if (string.IsNullOrEmpty(jwtToken))
                return NoContent();//set information about wrong credentials

            return Ok(jwtToken);
        }

        [AllowAnonymous]
        [HttpPost("AccountCreate")]
        //[CreateAccountFilter] //Otan exw to filter den ftanei to request
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