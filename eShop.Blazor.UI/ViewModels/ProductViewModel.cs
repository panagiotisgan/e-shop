using Blazored.LocalStorage;
using eShop.Blazor.UI.Dto_Model;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.ViewModels
{
    public class ProductViewModel : IProductViewModel
    {
        private HttpClient _client;
        private ILocalStorageService _localStorageService;
        private readonly AuthenticationStateProvider authenticationStateProvider;
        public IEnumerable<Product> Products { get; set;}

        public ProductViewModel(HttpClient client, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _client = client;
            _localStorageService = localStorageService;
            this.authenticationStateProvider = authenticationStateProvider;
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
            await GetProductsAsync();
        }

        public async Task<Product> GetByIdAsync(long productId)
        {
            try
            {
                var token = await _localStorageService.GetItemAsync<string>("jwt_token");
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
                var response = await this._client.GetAsync($"api/Products/GetProduct/{productId}");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var product = JsonConvert.DeserializeObject<Product>(result);
                    return product;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return null;
        }

        public async Task GetProductsAsync()
        {
            try
            {
                var token = await _localStorageService.GetItemAsync<string>("jwt_token");
                //var cookie = await _localStorageService.GetItemAsync<AuthenticationResult>("jwt.cookie");
                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            }
            catch(Exception ex)
            {

            }
            //requestMessage.Method = HttpMethod.Get;
            //requestMessage.RequestUri = new System.Uri("https://localhost:44371/api/Products/GetProducts");
            //requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", cookie.Token);

            


            //var result = await _client.SendAsync(requestMessage); 

            var result = await _client.GetAsync("api/Products/GetProducts");
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                Products = JsonConvert.DeserializeObject<IEnumerable<Product>>(content);
            }
        }
    }
}
