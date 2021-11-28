using eShop.Blazor.UI.Dto_Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public class UserService : IUserService
    {
        private HttpClient _client;
        public UserService(HttpClient client)
        {
            _client = client;
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
    }
}
