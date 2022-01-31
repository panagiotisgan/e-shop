using eShop.Blazor.UI.Dto_Model;
using System.Threading.Tasks;

namespace eShop.Blazor.UI
{
    public interface IAuthenticationService
    {
        Task<AuthenticatedUserModel> Login(AuthenticationUserModel authenticationUser);
        Task Logout();
    }
}