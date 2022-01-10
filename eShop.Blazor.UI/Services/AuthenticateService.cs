using Blazored.LocalStorage;
using eShop.Blazor.UI.Dto_Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        public AuthenticationResult AuthResult { get; private set; }

        private readonly HttpClient httpClient;
        private ILocalStorageService _localStorageService;
        public AuthenticateService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            this.httpClient = httpClient;
            this._localStorageService = localStorageService;
        }
        public async Task AuthenticateAsync(string Username, string Password)
        {
            var userCredentials = new
            {
               Username = Username,
               Password = Password,
               IsHuman = true
            };

            var jsonObject = JsonConvert.SerializeObject(userCredentials);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonObject);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            try
            {
                var result = await httpClient.PostAsync("api/Users/authenticate", byteContent);
                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    AuthResult = JsonConvert.DeserializeObject<AuthenticationResult>(content);
                    await _localStorageService.SetItemAsync<AuthenticationResult>("jwt.cookie", AuthResult);
                }
            }
            catch(Exception ex)
            {

            }
            //var user = await result.Content.ReadFromJsonAsync<AuthenticationResult>();            
        }

        public async Task InitializeCookie()
        {
            AuthResult = await _localStorageService.GetItemAsync<AuthenticationResult>("jwt.cookie");
        }
    }
}
