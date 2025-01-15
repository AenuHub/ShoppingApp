using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingApp.Business.Operations.User;
using ShoppingApp.Business.Operations.User.Dtos;
using ShoppingApp.WebApi.Jwt;
using ShoppingApp.WebApi.Models;

namespace ShoppingApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } // TODO: action filter will be added

            var addUserDto = new AddUserDto
            {
                Email = request.Email,
                Password = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userService.AddUserAsync(addUserDto);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return BadRequest(result.Message);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            } // TODO: action filter will be added

            var result = await _userService.LoginUserAsync(new LoginUserDto
            {
                Email = request.Email,
                Password = request.Password
            });

            if (!result.IsSuccess) return BadRequest(result.Message);

            var user = result.Data;

            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>();

            var token = JwtHelper.GenerateToken(new JwtDto
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Key = configuration["Jwt:Key"]!,
                Issuer = configuration["Jwt:Issuer"]!,
                Audience = configuration["Jwt:Audience"]!,
                DurationInMinutes = int.Parse(configuration["Jwt:DurationInMinutes"]!)
            });

            return Ok(new LoginResponse
            {
                Message = "Login successful",
                Token = token
            });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            return Ok();
        }
    }
}
