using eShop.Blazor.UI.Dto_Model;
using eShop.Blazor.UI.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public interface IOrderService
    {
        public Task<(OrderPaginationDTO orderList,PaginationMetaDataResult paginationData)> GetOrdersAsync(int page = 1);
    }
}
