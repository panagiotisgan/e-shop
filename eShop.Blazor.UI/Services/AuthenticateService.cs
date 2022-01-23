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
        private readonly ITokenRepository _tokenRepository;
        public AuthenticateService(HttpClient httpClient, IWebApiHelper webApiHelper, ITokenRepository tokenRepository)
        {
            this.httpClient = httpClient;
            _webApiHelper = webApiHelper;
            _tokenRepository = tokenRepository;
            //this._localStorageService = localStorageService;
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



            var jsonObject = JsonConvert.SerializeObject(userCredentials);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonObject);
            //var byteContent = new ByteArrayContent(buffer);
            var stringContent = new StringContent(jsonObject,Encoding.UTF8, "application/json");
            //byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            try
            {
                //var result = await httpClient.PostAsync("api/Users/authentication", stringContent);
                apiResult = await _webApiHelper.InvokePostReturnString("api/Users/authentication", userCredentials);
                //await this._tokenRepository.SetToken(apiResult);
                //if (result.IsSuccessStatusCode)
                //{
                //    apiResult = await result.Content.ReadAsStringAsync();
                //    //AuthResult = JsonConvert.DeserializeObject<AuthenticationResult>(content);
                //    //await _localStorageService.SetItemAsync<AuthenticationResult>("jwt.cookie", AuthResult);
                //}
            }
            catch(Exception ex)
            {

            }

            return apiResult;          
        }
    }
}
