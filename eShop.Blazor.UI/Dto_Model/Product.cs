using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.Blazor.UI.Dto_Model
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal StockQty { get; set; }
        public bool InStock { get; set; }
        public decimal Price { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public List<Image> Images { get; set; } = new List<Image>();        
    }
}
