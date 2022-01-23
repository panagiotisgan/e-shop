using eShop.Blazor.UI.Dto_Model;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Services
{
    public interface IAuthenticateService
    {
        Task<string> AuthenticateAsync(string Username, string Password);
    }
}
