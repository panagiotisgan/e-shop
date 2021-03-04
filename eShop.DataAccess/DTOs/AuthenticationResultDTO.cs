using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.DataAccess.DTOs
{
    public class AuthenticationResultDTO
    {
        public long UserId { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string ErrorMessage { get; set; }
    }
}
