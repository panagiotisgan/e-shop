using eShop.DataAccess.AdditionalDetailsModels;
using eShop.DataAccess.DTOs;
using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShop.DataAccess.IServices
{
    public interface ILoginService
    {
        Task<bool> CheckCredentials(string username, string password, bool isHuman);
    }
}
