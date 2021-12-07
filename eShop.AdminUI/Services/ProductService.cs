using eShop.AdminUI.DtoModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace eShop.AdminUI.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient httpClient;

        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            //httpClient.BaseAddress = new Uri("https://localhost:44371/");
            //List<Product> productList = new List<Product>();
            try
            {
                return await this.httpClient.GetFromJsonAsync<Product[]>("api/Products/GetProducts");
            }
            catch(Exception ex){
                Console.WriteLine(ex.Message);
            }

            return null;
                //JsonConvert.DeserializeObject<List<Product>>(await productList.Content.ReadAsStringAsync());

        }
    }
}
