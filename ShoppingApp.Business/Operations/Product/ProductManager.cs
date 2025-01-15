using ShoppingApp.Business.Operations.Product.Dtos;
using ShoppingApp.Business.Types;
using ShoppingApp.Data.Entities;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Data.UnitOfWork;

namespace ShoppingApp.Business.Operations.Product
{
    public class ProductManager : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<ProductEntity> _repository;

        public ProductManager(IUnitOfWork unitOfWork, IRepository<ProductEntity> repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        public async Task<ServiceMessage> AddProductAsync(AddProductDto addProductDto)
        {
            var hasProduct = _repository.GetAll(p => p.ProductName.ToLower() == addProductDto.ProductName.ToLower()).Any();

            if (hasProduct)
            {
                return new ServiceMessage
                {
                    IsSuccess = false,
                    Message = "Product already exists"
                };
            }

            var productEntity = new ProductEntity
            {
                ProductName = addProductDto.ProductName,
                Price = addProductDto.Price,
                StockQuantity = addProductDto.StockQuantity
            };

            _repository.Add(productEntity);
            try
            {
                await _unitOfWork.SaveChangesAsync();
                return new ServiceMessage
                {
                    IsSuccess = true
                };
            }
            catch (Exception)
            {
                throw new Exception("Product could not be added");
            }
        }
    }
}
