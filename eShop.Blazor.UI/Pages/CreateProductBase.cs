using eShop.Blazor.UI.Dto_Model;
using eShop.Blazor.UI.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Pages
{
    public class CreateProductBase : ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
        [Inject]
        private ICategoryService CategoryService { get; set; }

        public List<Category> Categories { get; set; } = new List<Category>(); 
        public Product Product { get; set; } = new Product();        

        protected async override Task OnInitializedAsync()
        {
            Categories = (await CategoryService.GetCategoriesAsync()).ToList();
        }
    }
}
