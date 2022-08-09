using eShop.Blazor.UI.Dto_Model;
using eShop.Blazor.UI.Services;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Pages.Admin
{
    public class OrdersBase : ComponentBase
    {
        [Inject]
        public IOrderService OrderService { get; set; }
        public IEnumerable<OrderDetails> Orders { get; set; }
        public int Pages { get; set; }
        protected int CurrentPage { get; set; } = 1; 

        protected override async Task OnInitializedAsync()
        {
            var result  = await OrderService.GetOrdersAsync();
            if (result == null)
                return;

            Orders = result.list;
        }
    }
}
