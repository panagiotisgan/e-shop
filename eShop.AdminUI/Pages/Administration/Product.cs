using eShop.AdminUI.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.AdminUI.Pages.Administration
{
    public partial class Product
    {
        [Inject]
        public IProductService ProductService { get; set; }
        public List<eShop.AdminUI.DtoModels.Product> Products { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Products = (await ProductService.GetProductsAsync()).ToList();
        }
    }
}
