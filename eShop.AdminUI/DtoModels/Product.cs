using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eShop.AdminUI.DtoModels
{
    public class Product :BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal StockQty { get; set; }
        public bool InStock { get; set; }

        public decimal Price { get; set; }
        /*Να κοοτάξω ξανά πως θα βάλω το παρακάτω πεδίο, όχι string μάλλον*/
        //public List<Image> Images { get; set; } = new List<Image>();
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
