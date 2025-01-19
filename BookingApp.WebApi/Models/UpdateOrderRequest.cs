using ShoppingApp.Business.Operations.Order.Dtos;

namespace ShoppingApp.WebApi.Models
{
    public class UpdateOrderRequest
    {
        public ICollection<CreateOrderProductDto>? OrderProducts { get; set; }
    }
}
