using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.DTOs
{
    public class OrderDetailsDTO
    {
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
