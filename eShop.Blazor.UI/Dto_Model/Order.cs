using System;

namespace eShop.Blazor.UI.Dto_Model
{
    public class Order
    {
        public OrderStatus OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public decimal TotalCost { get; set; }
        public bool Invoice { get; set; }
        public long UserId { get; set; }
        public User User { get; set; }
    }

    public enum OrderStatus
    {
        Pending = 0,
        Editing = 1,
        Payment_Approval = 2,
        Delivered = 3
    }
}
