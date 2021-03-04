using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.DataAccess.AdditionalDetailsModels
{
    public class CreateAccountErrorsModel
    {
        public List<string> ErrorMessage { get; set; }
        public bool IsValid { get; set; }
    }
}
