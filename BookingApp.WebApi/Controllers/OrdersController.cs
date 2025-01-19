using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Business.Operations.Order;
using ShoppingApp.Business.Operations.Order.Dtos;
using ShoppingApp.WebApi.Filters;
using ShoppingApp.WebApi.Models;

namespace ShoppingApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var result = await _orderService.GetOrderInfoAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("all-orders")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrdersAsync();
            return Ok(result);
        }

        [HttpPost("create-order")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            var createOrderDto = new OrderDto
            {
                OrderDate = DateTime.Now,
                CustomerId = request.CustomerId,
                OrderProducts = request.OrderProducts
            };

            var result = await _orderService.CreateOrderAsync(createOrderDto);
            return (result.IsSuccess) ? Ok() : BadRequest(result.Message);
        }

        [HttpPut("update-order/{id}")]
        [Authorize(Roles = "Customer,Admin")]
        [TimeControlFilter]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderRequest request)
        {
            var updateOrderDto = new UpdateOrderDto
            {
                Id = id,
                OrderProducts = request.OrderProducts
            };

            var result = await _orderService.UpdateOrderAsync(id, updateOrderDto);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok();
        }

        [HttpPatch("patch-order/{id}")]
        [Authorize(Roles = "Customer,Admin")]
        public async Task<IActionResult> PatchOrder(int id, DateTime date)
        {
            var order = await _orderService.GetOrderAsync(id);
            if (order is null) return NotFound($"The order with id: {id} is not found.");

            var updateOrderDto = new OrderDto
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                CustomerId = order.CustomerId,
                OrderProducts = order.OrderProducts,
                OrderDate = date
            };

            var result = await _orderService.PatchOrderAsync(id, updateOrderDto);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok();
        }

        [HttpDelete("delete-order/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrderAsync(id);
            if (!result.IsSuccess) return NotFound(result.Message);
            return Ok();
        }

        [HttpGet("throw-exception")]
        public IActionResult ThrowException()
        {
            throw new Exception("This is a test exception.");
        }
    }
}
