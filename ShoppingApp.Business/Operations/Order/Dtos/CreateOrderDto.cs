using ShoppingApp.Data.Entities;

namespace ShoppingApp.Business.Operations.Order.Dtos
{
    public class CreateOrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public UserEntity Customer { get; set; }
        public List<int> ProductIds { get; set; }
    }
}
