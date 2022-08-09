using eShop.DataAccess.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.Helpers
{
    internal static class Calculator
    {
        public static decimal CalculateOrderTotalCost(this List<OrderDetailsDTO> orderDetails)
        {
            decimal total = 0;
            foreach (var order in orderDetails)
            {
                 total += order.Quantity * order.UnitPrice;
            }

            return total;            
        }
    }
}
