using eShop.Blazor.UI.Dto_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
