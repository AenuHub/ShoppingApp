namespace ShoppingApp.Business.Operations.Order.Dtos
{
    public class OrderInfoDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string CustomerName { get; set; }
        public ICollection<OrderProductInfoDto> OrderProducts { get; set; }
    }
}
