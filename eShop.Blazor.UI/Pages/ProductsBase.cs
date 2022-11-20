using eShop.Blazor.UI.Dto_Model;
using eShop.Blazor.UI.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Pages
{
    public class ProductsBase : ComponentBase
    {
        //private HttpRequestMessage _httpRequest;
        
        [Inject]
        public IProductService ProductService { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public string Value { get; set; }

        protected override async Task OnInitializedAsync()
        {
            //_httpRequest = new HttpRequestMessage();
            Products = (await ProductService.GetProductsAsync()).ToList();
        }

        public async Task DeleteProduct(long productId)
        {
            //_httpRequest = new HttpRequestMessage();
            await ProductService.DeleteProduct(productId);
            Products = (await ProductService.GetProductsAsync()).ToList();
        }       
    }
}
