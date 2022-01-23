namespace eShop.WebApi.Auth
{
    public interface ICustomManager
    {
        string CreateToken(string username);
        void ResetToken();
        bool VerifyToken(string token);
        string GetUserByToken(string token);
    }
}
