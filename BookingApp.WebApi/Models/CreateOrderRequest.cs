using ShoppingApp.Business.Operations.Order.Dtos;

namespace ShoppingApp.WebApi.Models
{
    public class CreateOrderRequest
    {
        public int CustomerId { get; set; }
        public ICollection<CreateOrderProductDto> OrderProducts { get; set; }
    }
}
