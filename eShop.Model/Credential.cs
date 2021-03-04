using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Model
{
    public class Credential:BaseEntity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
