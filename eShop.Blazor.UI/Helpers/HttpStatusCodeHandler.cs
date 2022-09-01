using Microsoft.AspNetCore.Components;
using System.Net;
using System.Runtime.CompilerServices;

namespace eShop.Blazor.UI.Helpers
{
    public class HttpStatusCodeHandler
    {
        private readonly NavigationManager _navigationManager;
        public HttpStatusCodeHandler(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }
        public void ReturnRespones(HttpStatusCode statusCode)
        {
            switch (statusCode)
            {
                case HttpStatusCode.NotFound: _navigationManager.NavigateTo("/NotFound");
                    break;
                case HttpStatusCode.Unauthorized: _navigationManager.NavigateTo("/Unauthorized");
                    break;
            }
        }
    }
}
