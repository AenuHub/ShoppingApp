using ShoppingApp.Business.Operations.Order.Dtos;
using ShoppingApp.Business.Types;

namespace ShoppingApp.Business.Operations.Order
{
    public interface IOrderService
    {
        Task<ServiceMessage> CreateOrderAsync(OrderDto createOrderDto);
        Task<OrderInfoDto> GetOrderInfoAsync(int id); // Renamed to avoid conflict
        Task<OrderDto> GetOrderAsync(int id);
        Task<List<OrderInfoDto>> GetAllOrdersAsync();
        Task<ServiceMessage> UpdateOrderAsync(int id, UpdateOrderDto updateOrderDto);
        Task<ServiceMessage> PatchOrderAsync(int id, OrderDto orderDto);
        Task<ServiceMessage> DeleteOrderAsync(int id);
    }
}
