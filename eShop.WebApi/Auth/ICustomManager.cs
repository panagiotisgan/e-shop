
using System.Threading.Tasks;

namespace eShop.WebApi.Auth
{
    public interface ICustomManager
    {
        Task<string> CreateToken(string username);
        void ResetToken();
        bool VerifyToken(string token);
        string GetUserByToken(string token);
    }
}
