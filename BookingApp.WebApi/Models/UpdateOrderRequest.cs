using ShoppingApp.Business.Operations.Order.Dtos;

namespace ShoppingApp.WebApi.Models
{
    public class UpdateOrderRequest
    {
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public ICollection<CreateOrderProductDto> OrderProducts { get; set; }
    }
}
