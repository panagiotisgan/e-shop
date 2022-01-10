namespace eShop.Blazor.UI.Dto_Model
{
    public class AuthenticationResult
    {
        public long UserId { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string ErrorMessage { get; set; }
    }
}
