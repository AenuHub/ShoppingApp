﻿using ShoppingApp.Business.Operations.Order.Dtos;

namespace ShoppingApp.WebApi.Models
{
    public class CreateOrderRequest
    {
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public ICollection<CreateOrderProductDto> OrderProducts { get; set; }
    }
}
