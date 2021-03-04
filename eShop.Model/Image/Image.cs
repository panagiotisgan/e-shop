using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.Model
{
    public class Image : BaseEntity
    {
        public long ProductId { get; set; }
        public string ImagePath { get; set; }
        public string TitleAttribute { get; set; }
    }
}
