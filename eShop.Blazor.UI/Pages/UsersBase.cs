using eShop.Blazor.UI.Dto_Model;
using eShop.Blazor.UI.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Pages
{
    public class UsersBase : ComponentBase
    {
        [Inject]
        public IUserService UserService { get; set; }
        public IEnumerable<User> Users { get; set; }

        protected async override Task OnInitializedAsync()
        {
            Users = (await UserService.GetUsersAsync()).ToList();
        }
    }
}
