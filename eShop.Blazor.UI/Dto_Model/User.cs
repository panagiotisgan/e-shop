using Radzen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Dto_Model
{
    public class User
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public Country Country { get; set; }        
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
        //public Credential Credential { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string ButtonLabel => IsActiveAccount ? "Disable" : "Enable";

        //public ButtonStyle StyleOfButton
        //{
        //    get
        //    {
        //        return IsActiveAccount ? ButtonStyle.Danger : ButtonStyle.Success;
        //    }
        //}

        public string ButtonClass => IsActiveAccount ? "btn btn-danger" : "btn btn-success";
    }
}
