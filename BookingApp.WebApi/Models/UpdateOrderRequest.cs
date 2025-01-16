using ShoppingApp.Business.Operations.Order.Dtos;
using System.ComponentModel.DataAnnotations;

namespace ShoppingApp.WebApi.Models
{
    public class UpdateOrderRequest
    {
        [Required]
        public int Id { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public ICollection<CreateOrderProductDto> OrderProducts { get; set; }
    }
}
