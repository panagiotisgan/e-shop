using System.Threading.Tasks;

namespace eShop.Blazor.UI.ViewModels
{
    public interface IOrderViewModel
    {
        public Task GetOrdersAsync();
    }
}
