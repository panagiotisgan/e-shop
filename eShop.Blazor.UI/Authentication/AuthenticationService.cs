using Blazored.LocalStorage;
using eShop.Blazor.UI.Dto_Model;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;

namespace eShop.Blazor.UI
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient client;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        private readonly ILocalStorageService _localStorageService;

        public AuthenticationService(HttpClient client, AuthenticationStateProvider authenticationStateProvider, ILocalStorageService localStorageService)
        {
            this.client = client;
            this.authenticationStateProvider = authenticationStateProvider;
            _localStorageService = localStorageService;
        }

        public async Task<AuthenticatedUserModel> Login(AuthenticationUserModel authenticationUser)
        {
            var data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string,string>("grant_type","password"),
                new KeyValuePair<string,string>("username",authenticationUser.Username),
                new KeyValuePair<string,string>("password",authenticationUser.Password),
                new KeyValuePair<string,string>("isHuman","true")
            });

            //var data = new
            //{
            //    Username = authenticationUser.Username,
            //    Password = authenticationUser.Password,
            //    IsHuman = true
            //};

            //var jsonObject = JsonConvert.SerializeObject(data);
            //var buffer = System.Text.Encoding.UTF8.GetBytes(jsonObject);
            //var byteContent = new ByteArrayContent(buffer);
            //var stringContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            var authResult = await this.client.PostAsync("https://localhost:44371/api/Users/authentication", data);
            var authContent = await authResult.Content.ReadAsStringAsync();

            if (authResult.IsSuccessStatusCode == false)
            {
                return null;
            }

            AuthenticatedUserModel userModel = new AuthenticatedUserModel()
            {
                Access_Token = authContent,
                Username = authenticationUser.Username

            };

            //var result = JsonSerializer.Deserialize<AuthenticatedUserModel>(authContent);

            await _localStorageService.SetItemAsync<string>("jwt_token", authContent);

            ((JwtTokenAuthenticationStateProvider)authenticationStateProvider).NotifyUserAuthentication(authContent);

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authContent);

            return userModel;
        }


        public async Task Logout()
        {
            await _localStorageService.RemoveItemAsync("jwt_token");
            ((JwtTokenAuthenticationStateProvider)authenticationStateProvider).NotifyUserLogout();
            client.DefaultRequestHeaders.Authorization = null;
        }

    }
}
