using eShop.DataAccess.AdditionalDetailsModels;
using eShop.DataAccess.DTOs;
using eShop.DataAccess.Helpers;
using eShop.DataAccess.IRepositories;
using eShop.DataAccess.IServices;
using eShop.DataAccess.Services;
using eShop.Model;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eShop.DataAccess
{
    public class LoginService : ILoginService
    {
        private readonly IUserUnitOfWork _userUnitOfWork;
        private readonly ICredentialRepository _credentialRepository;
        private const string _SECRET = "xVigzSeBiGNefeojZro3";
        //private readonly AppSettings _appSettings;

        public LoginService(IUserUnitOfWork userUnitOfWork, ICredentialRepository credentialRepository/*,IOptions<AppSettings> appSettings*/)
        {
            this._userUnitOfWork = userUnitOfWork;
            this._credentialRepository = credentialRepository;
            //this._appSettings = appSettings.Value;
        }

        public (AuthenticationResultDTO user, CreateAccountErrorsModel accountErrors) PasswordSignIn(string username, string password, bool isHuman)
        {
            CreateAccountErrorsModel createAccountErrors = new CreateAccountErrorsModel();

            createAccountErrors.IsValid = true;
            createAccountErrors.ErrorMessage = new List<string>();

            AuthenticationResultDTO userResult = new AuthenticationResultDTO();

            const string ERROR_MSG = "Invalid Username or password.Try again!";
            string errorMsg = null;

            //if (!isHuman)
            //{
            //    createAccountErrors.ErrorMessage.Add(ERROR_MSG);
            //    createAccountErrors.IsValid = false;
            //    return (userResult, createAccountErrors);
            //}

            //if (String.IsNullOrWhiteSpace(password))
            //{
            //    if (errorMsg == null)
            //        errorMsg = ERROR_MSG;

            //    createAccountErrors.ErrorMessage.Add(ERROR_MSG);
            //    createAccountErrors.IsValid = false;
            //    return (userResult, createAccountErrors);
            //}

            var accountExist = _credentialRepository.GetByName(username);
            if (accountExist == null)
            {
                errorMsg = ERROR_MSG;
                createAccountErrors.ErrorMessage.Add(ERROR_MSG);
                createAccountErrors.IsValid = false;
                return (userResult, createAccountErrors);
            }

            var user = _userUnitOfWork.UserDbRepository.GetUserByCredentialId(accountExist.Id);

            if (!user.IsActiveAccount)
            {
                errorMsg = "You have receive an verification link in your email.Please activate your account.";
                createAccountErrors.ErrorMessage.Add(errorMsg);
                return (userResult, createAccountErrors);
            }

            var hash = PasswordGenerator.Hash(password, Convert.FromBase64String(accountExist.Salt));

            var realPassword = Convert.ToBase64String(hash);

            if (realPassword != accountExist.Password)
            {
                if (errorMsg == null)
                {
                    errorMsg = ERROR_MSG;
                }
                createAccountErrors.ErrorMessage.Add(errorMsg);
                createAccountErrors.IsValid = false;
                return (userResult, createAccountErrors);
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_SECRET);

            //Παίζει και με τον παρακάτω κώδικα που έχω σε comment και με αυτόν που χρησιμοποιώ παρακάτω
            var jwtSecurityToken = new JwtSecurityToken(

               claims: new[]
               {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
               },
               expires: DateTime.UtcNow.AddHours(2),
               signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
               );

            var token = tokenHandler.WriteToken(jwtSecurityToken);
            
            userResult.IsAuthenticated = true;
            userResult.Role = user.Role;
            userResult.UserId = user.Id;
            userResult.Token = token;

            return (userResult, createAccountErrors);
        }
    }
}

