using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.UI.ViewModels
{
    public class ProductListVM
    {
        public long ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public string ImagePath { get; set; }
    }
}
