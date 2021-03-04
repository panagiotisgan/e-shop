using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.DTOs
{
    public class UserUpdateDTO
    {
        public long UserId { get; set; }

        #region User Details
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VATNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressNo1 { get; set; }
        public string AddressNo2 { get; set; }
        public Country Country { get; set; }
        #endregion User Details
    }
}
