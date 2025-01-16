using ShoppingApp.Business.Operations.Order.Dtos;
using ShoppingApp.Business.Types;
using ShoppingApp.Data.Entities;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Data.UnitOfWork;

namespace ShoppingApp.Business.Operations.Order
{
    public class OrderManager : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<OrderEntity> _orderRepository;
        private readonly IRepository<OrderProductEntity> _orderProductRepository;

        public OrderManager(IUnitOfWork unitOfWork, IRepository<OrderEntity> orderRepository, IRepository<OrderProductEntity> orderProductRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
        }

        public async Task<ServiceMessage> CreateOrderAsync(CreateOrderDto createOrderDto)
        {
            var order = _orderRepository.GetAll(o => o.Id == createOrderDto.Id).Any();

            if (order)
            {
                return new ServiceMessage
                {
                    IsSuccess = false,
                    Message = "Order already exists."
                };
            }

            await _unitOfWork.BeginTransactionAsync();
            var orderEntity = new OrderEntity
            {
                Id = createOrderDto.Id,
                OrderDate = createOrderDto.OrderDate,
                TotalAmount = createOrderDto.TotalAmount,
                CustomerId = createOrderDto.CustomerId,
                Customer = createOrderDto.Customer
            };

            _orderRepository.Add(orderEntity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while creating the order.");
            }

            foreach (var productId in createOrderDto.ProductIds)
            {
                var orderProduct = new OrderProductEntity
                {
                    OrderId = orderEntity.Id,
                    ProductId = productId
                };

                _orderProductRepository.Add(orderProduct);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception("An error occurred while adding products to the order. Transaction rolled back.");
            }

            return new ServiceMessage
            {
                IsSuccess = true
            };
        }
    }
}
