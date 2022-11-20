using eShop.Blazor.UI.Dto_Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using System.Net.Http.Json;

namespace eShop.Blazor.UI.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;
        private HttpRequestMessage HttpRequestMessage;
        public UserService(HttpClient client)
        {
            _client = client;
            //HttpRequestMessage = new()
            //{
            //    RequestUri = new Uri("api/Users/GetUsers"),
            //    Method = HttpMethod.Post
            //};
        }
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var result = await _client.GetAsync("api/Users");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<User>>(content);
            }

            return null;
        }

        public async Task<bool> SetUserAccountState(User user)
        {          

            //this.HttpRequestMessage = new()
            //{
            //    RequestUri = new Uri("api/Users/UpdateState"),
            //    Method = HttpMethod.Post,
            //    Content = JsonContent.Create(new { Id= user.Id })
            //};

            try
            {
                //var result = await _client.SendAsync(HttpRequestMessage);
                var result = await _client.PostAsync("api/Users/UpdateState", JsonContent.Create(new { Id = user.Id }));
                result.EnsureSuccessStatusCode();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

            throw new NotImplementedException();
        }
    }
}
