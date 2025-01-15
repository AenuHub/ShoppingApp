using ShoppingApp.Business.DataProtection;
using ShoppingApp.Business.Operations.User.Dtos;
using ShoppingApp.Business.Types;
using ShoppingApp.Data.Entities;
using ShoppingApp.Data.Repositories;
using ShoppingApp.Data.UnitOfWork;

namespace ShoppingApp.Business.Operations.User
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtection _protection;

        public UserManager(IUnitOfWork unitOfWork, IRepository<UserEntity> userRepository, IDataProtection protection)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _protection = protection;
        }

        public async Task<ServiceMessage> AddUserAsync(AddUserDto addUserDto)
        {
            var hasMail = _userRepository.GetAll(u => u.Email.ToLower() == addUserDto.Email.ToLower());

            if (hasMail.Any())
            {
                return new ServiceMessage
                {
                    IsSuccess = false,
                    Message = "Email already exists"
                };
            }

            var userEntity = new UserEntity()
            {
                Email = addUserDto.Email,
                FirstName = addUserDto.FirstName,
                LastName = addUserDto.LastName,
                Password = _protection.Encrypt(addUserDto.Password),
                PhoneNumber = addUserDto.PhoneNumber,
                Role = Data.Enums.UserRole.Customer
            };

            _userRepository.Add(userEntity);
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
                throw new Exception("User could not be added");
            }
        }

        public async Task<ServiceMessage<UserInfoDto>> LoginUserAsync(LoginUserDto loginUserDto)
        {
            var userEntity = _userRepository.Get(u => u.Email.ToLower() == loginUserDto.Email.ToLower());

            if (userEntity == null || _protection.Decrypt(userEntity.Password) != loginUserDto.Password)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSuccess = false,
                    Message = "Email or password is incorrect"
                };
            }

            var userInfoDto = new UserInfoDto
            {
                Email = userEntity.Email,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                Role = userEntity.Role
            };

            return new ServiceMessage<UserInfoDto>
            {
                IsSuccess = true,
                Data = userInfoDto
            };
        }
    }
}
