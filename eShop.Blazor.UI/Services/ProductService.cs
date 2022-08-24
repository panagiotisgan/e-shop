using Blazored.LocalStorage;
using eShop.Blazor.UI.Dto_Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public class ProductService : IProductService
    {
        private HttpClient _client;
        private ILocalStorageService _localStorageService;
        public ProductService(HttpClient client, ILocalStorageService localStorageService)
        {
            _client = client;
            _localStorageService = localStorageService;
        }

        public async Task CreateOrUpdateProductAsync(Product product)
        {
            var jsonObject = JsonConvert.SerializeObject(product);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonObject);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            if (product.Id > 0)
                await this._client.PutAsync("api/Products", byteContent);
           else
                await this._client.PostAsync("api/Products", byteContent);
        }

        public async Task DeleteProduct(long productId)
        {
            await _client.DeleteAsync($"api/Products/delete?productId={productId}");
        }

        public async Task<Product> GetByIdAsync(long productId)
        {
            try
            {
                var response = await this._client.GetAsync($"api/Products/GetProduct/{productId}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<Product>(result);
                    return product;
                }
            }
            catch (Exception)
            {
                return null;
            }

            return null;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("jwt_token");
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            //requestMessage.Method = HttpMethod.Get;
            //requestMessage.RequestUri = new System.Uri("api/Products/GetProducts");
            //requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", cookie.Token);

            //var response = await _client.SendAsync(requestMessage); 

            var result = await _client.GetAsync("api/Products/GetProducts");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<Product>>(content);
            }

            return null;
        }
    }
}
