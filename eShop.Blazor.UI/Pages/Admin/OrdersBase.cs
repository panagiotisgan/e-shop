using eShop.Blazor.UI.Dto_Model;
using eShop.Blazor.UI.Helpers;
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
        public PaginationMetaDataResult PaginationResult { get; set; }
        //public int Pages { get; set; }
        protected int CurrentPage { get; set; } = 1;

        protected async Task SelectedPage(int page)
        {
            PaginationResult.CurrentPage = page;
            var res = await OrderService.GetOrdersAsync(page);
            this.Orders = res.orderList.list;
            PaginationResult = res.paginationData;
        }

        protected override async Task OnInitializedAsync()
        {
            var result  = await OrderService.GetOrdersAsync();
            if (result.orderList == null)
                return;

            PaginationResult = result.paginationData;

            Orders = result.orderList.list;
        }
    }
}
