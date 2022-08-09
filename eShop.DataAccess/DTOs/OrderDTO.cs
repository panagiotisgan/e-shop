using System;
using System.Collections.Generic;
using System.Text;

namespace eShop.DataAccess.DTOs
{
    public class OrderDTO
    {
        public long UserId { get; set; }
        public bool Invoice { get; set; }
        public List<OrderDetailsDTO> OrderDetailsDTOList { get; set; }
    }
}
