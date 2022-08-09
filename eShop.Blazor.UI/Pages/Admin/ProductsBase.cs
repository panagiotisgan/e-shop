using Blazored.LocalStorage;
using eShop.Blazor.UI.Components;
using eShop.Blazor.UI.Dto_Model;
using eShop.Blazor.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Pages.Admin
{
    public class ProductsBase : ComponentBase
    {
        
        [Inject]
        public IProductService ProductService { get; set; }
        public IEnumerable<Product> Products { get; set; }

        public ConfirmationModalComponent DeleteConfirmation { get; set; }
        
        public void Delete_Product()
        {
            DeleteConfirmation.Show();
            Console.WriteLine("Delete clicked");            
        }

        

        protected async override Task OnInitializedAsync()
        {
            Products = await ProductService.GetProductsAsync();
        }
    }
}
