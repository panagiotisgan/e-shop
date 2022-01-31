using System.ComponentModel.DataAnnotations;

namespace eShop.Blazor.UI.Dto_Model
{
    public class AuthenticationUserModel
    {
        [Required(ErrorMessage = "Your email is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public bool IsHuman { get; set; }
    }
}
