using eShop.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.DTOs
{
    /// <summary>
    /// Model to create user and credentials
    /// </summary>
    public class UserDTO
    {
        #region Credential Details
        public string Username { get; set; }
        public string Password { get; set; }
        #endregion Credential Details

        #region User Details
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VATNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressNo1 { get; set; }
        public string AddressNo2 { get; set; }
        public string ZipCode { get; set; }
        public string Role { get; set; }
        public Country Country { get; set; }
        #endregion User Details
    }
}
