using eShop.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.UI.ViewModels
{
    public class ProductVM
    {
        public List<IFormFile> Images { get; set; } = new List<IFormFile>();
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal StockQty { get; set; }
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
        public List<ProductListVM> TableData = new List<ProductListVM>();
        public List<SelectListItem> Categories { get; set; }
    }
}
