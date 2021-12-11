using eShop.Blazor.UI.Dto_Model;
using eShop.Blazor.UI.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Pages
{
    public class ProductsBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public string Value { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Products = (await ProductService.GetProductsAsync()).ToList();
        }

        public async Task DeleteProduct(long productId)
        {
            await ProductService.DeleteProduct(productId);
            Products = (await ProductService.GetProductsAsync()).ToList();
        }       
    }
}
