using System.Collections.Generic;

namespace eShop.Blazor.UI.Dto_Model
{
    public class OrderPaginationDTO
    {
        public List<OrderDetails> list { get; set; } = new List<OrderDetails>();
        public int count { get; set; }
    }
}
