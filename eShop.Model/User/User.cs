using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace eShop.Model
{
    public class User : BaseEntity
    {
        public const int PhoneMaxLength = 50;
        public const int AddressMaxLength = 120;
        public const int EmailMaxLength = 120;
        public const int NamesLength = 120;
        public const int VATLength = 50;
        public const int ZipCodeMaxLength = 50;
        public const int CityStateMaxLength = 50;


        public long CountryId { get; set; }
        
        public Country Country { get; set; }

        //public void SetCountry(Country country)
        //{
        //    this.CountryId = country.Id;
        //    this.Country = country;
        //}

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VATNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string AddressNo1 { get; set; }
        public string AddressNo2 { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public bool IsActiveAccount { get; set; }
        //public Role Role { get; set; }
        public string Role { get; set; }
        public long CredentialId { get; set; }
        public Credential Credential { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public ObservableCollection<Order> Orders { get; set; }
    }

    public static class Role
    {
        /// <summary>
        /// simple user
        /// </summary>
        public const string User = "User";
        /// <summary>
        /// for account with less privilages in admin side
        /// </summary>
        public const string Moderator = "Moderator";
        /// <summary>
        /// site Admin
        /// </summary>
        public const string Admin = "Admin";
        /// <summary>
        /// Indicate that a user can access an action if it's admin or moderator or both 
        /// </summary>
        public const string MultipleRoles = "Admin,User";
    }
}
