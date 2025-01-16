using Microsoft.EntityFrameworkCore;
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
        private readonly IRepository<ProductEntity> _productRepository;

        public OrderManager(IUnitOfWork unitOfWork, IRepository<OrderEntity> orderRepository, IRepository<OrderProductEntity> orderProductRepository, IRepository<ProductEntity> productRepository)
        {
            _unitOfWork = unitOfWork;
            _orderRepository = orderRepository;
            _orderProductRepository = orderProductRepository;
            _productRepository = productRepository;
        }

        public async Task<ServiceMessage> CreateOrderAsync(OrderDto createOrderDto)
        {
            var totalAmount = createOrderDto.OrderProducts
                .Select(op => _productRepository.GetById(op.Id).Price * op.Quantity)
                .Sum();

            await _unitOfWork.BeginTransactionAsync();

            var orderEntity = new OrderEntity
            {
                Id = createOrderDto.Id,
                OrderDate = createOrderDto.OrderDate,
                TotalAmount = totalAmount,
                CustomerId = createOrderDto.CustomerId
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

            foreach (var product in createOrderDto.OrderProducts)
            {
                var orderProduct = new OrderProductEntity
                {
                    OrderId = orderEntity.Id,
                    ProductId = product.Id,
                    Quantity = product.Quantity
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

        public async Task<OrderInfoDto> GetOrderInfoAsync(int id)
        {
            var order = await _orderRepository.GetAll(o => o.Id == id)
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync();

            if (order == null) return null;

            var orderInfo = new OrderInfoDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                CustomerName = $"{order.Customer.FirstName} {order.Customer.LastName}",
                OrderProducts = order.OrderProducts.Select(op => new OrderProductInfoDto
                {
                    Id = op.ProductId,
                    ProductName = op.Product.ProductName,
                    Quantity = op.Quantity
                }).ToList()
            };

            return orderInfo;
        }

        public async Task<OrderDto> GetOrderAsync(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null) return null;

            var orderDto = new OrderDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                CustomerId = order.CustomerId,
                OrderProducts = order.OrderProducts.Select(op => new CreateOrderProductDto
                {
                    Id = op.ProductId,
                    Quantity = op.Quantity
                }).ToList()
            };

            return orderDto;
        }

        public async Task<List<OrderInfoDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAll()
                .Include(o => o.Customer)
                .Include(o => o.OrderProducts)
                .ThenInclude(op => op.Product)
                .ToListAsync();

            var orderInfos = orders.Select(order => new OrderInfoDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                CustomerName = $"{order.Customer.FirstName} {order.Customer.LastName}",
                OrderProducts = order.OrderProducts.Select(op => new OrderProductInfoDto
                {
                    Id = op.ProductId,
                    ProductName = op.Product.ProductName,
                    Quantity = op.Quantity
                }).ToList()
            }).ToList();

            return orderInfos;
        }

        public async Task<ServiceMessage> UpdateOrderAsync(int id, UpdateOrderDto updateOrderDto)
        {
            var order = _orderRepository.GetAll(o => o.Id == updateOrderDto.Id)
                .Include(o => o.OrderProducts)
                .FirstOrDefault();

            if (order == null)
            {
                return new ServiceMessage
                {
                    IsSuccess = false,
                    Message = $"Order with id: {updateOrderDto.Id} is not found."
                };
            }

            await _unitOfWork.BeginTransactionAsync();

            // update the existing order
            var totalAmount = updateOrderDto.OrderProducts
                .Select(op => _productRepository.GetById(op.Id).Price * op.Quantity)
                .Sum();
            order.TotalAmount = totalAmount;
            order.CustomerId = updateOrderDto.CustomerId;

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception("An error occurred while updating the order. Transaction rolled back.");
            }

            // update the orderProducts
            foreach (var product in updateOrderDto.OrderProducts)
            {
                var existingOrderProduct = order.OrderProducts.FirstOrDefault(op => op.ProductId == product.Id);
                if (existingOrderProduct != null)
                {
                    _orderProductRepository.Delete(existingOrderProduct, false);
                }

                var orderProduct = new OrderProductEntity
                {
                    OrderId = order.Id,
                    ProductId = product.Id,
                    Quantity = product.Quantity
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
                throw new Exception("An error occurred while updating the order. Transaction rolled back.");
            }

            return new ServiceMessage
            {
                IsSuccess = true
            };
        }

        public async Task<ServiceMessage> PatchOrderAsync(int id, OrderDto orderDto)
        {
            var order = await _orderRepository.GetAll(o => o.Id == id).FirstOrDefaultAsync();
            if (order == null)
            {
                return new ServiceMessage
                {
                    IsSuccess = false,
                    Message = $"Order with id: {id} is not found."
                };
            }

            order.OrderDate = orderDto.OrderDate;
            _orderRepository.Update(order);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while updating the order.");
            }

            return new ServiceMessage
            {
                IsSuccess = true
            };
        }

        public async Task<ServiceMessage> DeleteOrderAsync(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                return new ServiceMessage
                {
                    IsSuccess = false,
                    Message = $"Order with id: {id} is not found."
                };
            }

            _orderRepository.Delete(order);
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("An error occurred while deleting the order.");
            }
            return new ServiceMessage
            {
                IsSuccess = true
            };
        }
    }
}
