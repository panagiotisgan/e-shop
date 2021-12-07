using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Dto_Model
{
    public class Image
    {
        public long ProductId { get; set; }
        public string ImagePath { get; set; }
        public string TitleAttribute { get; set; }
        public byte[] PictureInByte { get; set; }
    }
}
