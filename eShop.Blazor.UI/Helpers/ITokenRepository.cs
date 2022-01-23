using System.Threading.Tasks;

namespace eShop.Blazor.UI.Helpers
{
    public interface ITokenRepository
    {
        Task<string> GetToken();
        Task SetToken(string token);
    }
}