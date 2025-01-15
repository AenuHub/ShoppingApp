using ShoppingApp.Business.Operations.User.Dtos;
using ShoppingApp.Business.Types;

namespace ShoppingApp.Business.Operations.User
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUserAsync(AddUserDto addUserDto);

        Task<ServiceMessage<UserInfoDto>> LoginUserAsync(LoginUserDto loginUserDto);
    }
}
