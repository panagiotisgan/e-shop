using eShop.Blazor.UI.Dto_Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _client;
        public CategoryService(HttpClient client)
        {
            this._client = client;
        }
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var response = await _client.GetAsync("api/Categories");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Category>>(content);
            }

            return null;
        }
    }
}
