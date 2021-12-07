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
        public ProductService(HttpClient client)
        {
            _client = client;
        }

        public async Task CreateProductAsync(Product product)
        {
            var jsonObject = JsonConvert.SerializeObject(product);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonObject);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            await this._client.PostAsync("api/Products",byteContent);
        }

        public async Task DeleteProduct(long productId)
        {
            await _client.DeleteAsync($"api/Products/delete?productId={productId}");            
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
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
