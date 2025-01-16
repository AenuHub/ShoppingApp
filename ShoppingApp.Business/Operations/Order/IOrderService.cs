using ShoppingApp.Business.Operations.Order.Dtos;
using ShoppingApp.Business.Types;

namespace ShoppingApp.Business.Operations.Order
{
    public interface IOrderService
    {
        Task<ServiceMessage> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<OrderInfoDto> GetOrderAsync(int id);
    }
}
