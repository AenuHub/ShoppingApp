﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Business.Operations.Order;
using ShoppingApp.Business.Operations.Order.Dtos;
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
        public async Task<IActionResult> GetOrder(int id)
        {
            var result = await _orderService.GetOrderAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }

        [HttpGet("all-orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrdersAsync();
            return Ok(result);
        }

        [HttpPost("create-order")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateOrder(CreateOrderRequest request)
        {
            var createOrderDto = new CreateOrderDto
            {
                OrderDate = request.OrderDate,
                TotalAmount = request.TotalAmount,
                CustomerId = request.CustomerId,
                OrderProducts = (ICollection<CreateOrderProductDto>)request.OrderProducts
            };

            var result = await _orderService.CreateOrderAsync(createOrderDto);
            return (result.IsSuccess) ? Ok() : BadRequest(result.Message);
        }

        [HttpPatch("update-order/{id}")]
        public async Task<IActionResult> UpdateOrder(int id, UpdateOrderRequest request)
        {
            var updateOrderDto = new UpdateOrderDto
            {
                Id = request.Id,
                TotalAmount = request.TotalAmount,
                CustomerId = request.CustomerId,
                OrderProducts = request.OrderProducts
            };

            var result = await _orderService.UpdateOrderAsync(id, updateOrderDto);
            if (!result.IsSuccess) return BadRequest(result.Message);
            return Ok();
        }
    }
}
