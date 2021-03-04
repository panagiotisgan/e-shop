using eShop.DataAccess.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace eShop.UI.ViewModels
{
    public class CreateUserVM
    {
        public UserDTO userDTO { get; set; }
        public long CountryId { get; set; }
        public long CityId { get; set; }
        public long StateId { get; set; }
        public List<SelectListItem> Countries { get; set; }
    }
}
