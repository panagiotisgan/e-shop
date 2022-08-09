using eShop.Blazor.UI.Dto_Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public interface IOrderService
    {
        public Task<OrderPaginationDTO> GetOrdersAsync();
    }
}
