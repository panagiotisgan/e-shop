using eShop.DataAccess.AdditionalDetailsModels;
using eShop.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.IServices
{
    public interface ILoginService
    {
        (AuthenticationResultDTO user, CreateAccountErrorsModel accountErrors) PasswordSignIn(string username, string password, bool isHuman);
    }
}
