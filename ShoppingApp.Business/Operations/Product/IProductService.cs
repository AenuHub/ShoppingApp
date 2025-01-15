using ShoppingApp.Business.Operations.Product.Dtos;
using ShoppingApp.Business.Types;

namespace ShoppingApp.Business.Operations.Product
{
    public interface IProductService
    {
        Task<ServiceMessage> AddProductAsync(AddProductDto addProductDto);
    }
}
