using eShop.Blazor.UI.Dto_Model;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public interface IAuthenticateService
    {
        Task AuthenticateAsync(string Username, string Password);
        Task InitializeCookie();
    }
}
