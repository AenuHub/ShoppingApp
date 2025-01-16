namespace ShoppingApp.Business.Operations.Order.Dtos
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public ICollection<CreateOrderProductDto> OrderProducts { get; set; }
    }
}
