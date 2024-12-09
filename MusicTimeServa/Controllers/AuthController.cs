using Microsoft.AspNetCore.Mvc;
using MusicTimeServa.Model;
using MusicTimeServa.Services;

namespace MusicTimeServa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        public IUserService m_UserService { get; set; }

        public AuthController(IUserService userService)
        {
            m_UserService = userService;
        }

        [HttpPost("Register")]
        public ApiResponse Register(UserRegisterDTO userDto)
        {
            if (userDto == null || string.IsNullOrEmpty(userDto.Name) || string.IsNullOrEmpty(userDto.Email))
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "User cant be null",
                    Result = null
                };
            }

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
            };

            m_UserService.Register(user);

            return new ApiResponse
            {
                IsSuccess = true,
                Message = "User generated!",
                Result = user
            };
        }

        [HttpPost("Login")]
        public ApiResponse Login(UserLoginDTO userLoginDto)
        {
            var user = m_UserService.Login(userLoginDto);

            if (user is null)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "User cant be null",
                    Result = null
                };

            }

            return new ApiResponse
            {
                IsSuccess = true,
                Message = "Success",
                Result = user
            };
        }
    }
}
