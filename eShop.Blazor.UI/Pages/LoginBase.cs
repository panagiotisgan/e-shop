using eShop.Blazor.UI.Dto_Model;
using eShop.Blazor.UI.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Pages
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }
        [Parameter]
        public string Username { get; set; }
        [Parameter]
        public string Password { get; set; }
        [Parameter]
        public AuthenticationResult AuthResult { get; set; }
        [Inject]
        protected IAuthenticateService authService { get; set; }        

        protected async Task OnLogin()
        {
            await authService.AuthenticateAsync(Username, Password);
            NavigationManager.NavigateTo("products");
        }
    }
}
