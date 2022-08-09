using Blazored.LocalStorage;
using eShop.Blazor.UI.Dto_Model;
using eShop.Blazor.UI.Helpers;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        public AuthenticationResult AuthResult { get; private set; }

        private HttpClient httpClient;
        //private ILocalStorageService _localStorageService;
        private readonly IWebApiHelper _webApiHelper;
        public AuthenticateService(HttpClient httpClient, IWebApiHelper webApiHelper)
        {
            this.httpClient = httpClient;
            _webApiHelper = webApiHelper;
        }
        public async Task<string> AuthenticateAsync(string Username, string Password)
        {
            var userCredentials = new
            {
               Username = Username,
               Password = Password,
               IsHuman = true
            };
            
            string apiResult = string.Empty;

            try
            {
                apiResult = await _webApiHelper.InvokePostReturnString("api/Users/authentication", userCredentials);
            }
            catch(Exception)
            {

            }

            return apiResult;          
        }
    }
}
