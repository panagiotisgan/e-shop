namespace eShop.Blazor.UI.Dto_Model
{
    public class OrderDetails
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public decimal Quantity { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
