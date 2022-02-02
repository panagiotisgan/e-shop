using eShop.DataAccess.AdditionalDetailsModels;
using eShop.DataAccess.DTOs;
using eShop.DataAccess.Helpers;
using eShop.DataAccess.IRepositories;
using eShop.DataAccess.IServices;
using eShop.DataAccess.Services;
using eShop.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess
{
    public class LoginService : ILoginService
    {
        private readonly IUserUnitOfWork _userUnitOfWork;
        private readonly ICredentialUnitOfWork _credentialUnitOfWork;
        private const string _SECRET = "xVigzSeBiGNefeojZro3";

        public LoginService(IUserUnitOfWork userUnitOfWork, ICredentialUnitOfWork credentialUnitOfWork)
        {
            this._userUnitOfWork = userUnitOfWork;
            _credentialUnitOfWork = credentialUnitOfWork;
        }
    
        public async Task<bool> CheckCredentials(string username, string password, bool isHuman)
        {
            var accountExist = _credentialUnitOfWork .CredentialDbRepository.GetByName(username);
            if (accountExist == null) 
                return false;            

            var user = await _userUnitOfWork.UserDbRepository.GetUserByCredentialId(accountExist.Id);
            if (!user.IsActiveAccount)
                return false;

            var hash = PasswordGenerator.Hash(password, Convert.FromBase64String(accountExist.Salt));

            var realPassword = Convert.ToBase64String(hash);

            if (realPassword != accountExist.Password)
                return false;
            
            return true;

        }
    }
}

