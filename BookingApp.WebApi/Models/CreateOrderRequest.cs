namespace ShoppingApp.WebApi.Models
{
    public class CreateOrderRequest
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public List<int> ProductIds { get; set; }
    }
}
