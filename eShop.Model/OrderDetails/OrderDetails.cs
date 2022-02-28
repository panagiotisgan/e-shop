using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace eShop.Model
{
    public class OrderDetails: BaseEntity
    {
        public const int ProductNameMaxLength = 120;
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public string Product_Name { get; set; }
        public decimal Unit_Price { get; set; }
        public decimal Quantity { get; set; }

        //public ObservableCollection<Product> Products { get; set; }
    }
}
