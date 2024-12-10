using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicTimeServa.Model;
using MusicTimeServa.Model.DTOs.Request;
using MusicTimeServa.Model.DTOs.Response;
using MusicTimeServa.Services;

namespace MusicTimeServa.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService m_UserService;
        private readonly IMapper m_Mapper;

        public AuthController(IUserService userService, IMapper mapper)
        {
            m_UserService = userService;
            m_Mapper = mapper;
        }

        [HttpPost("Register")]
        public ApiResponse Register(UserRegisterRequestDTO userDto)
        {
            //TODO: More richer error message
            if (userDto == null || string.IsNullOrEmpty(userDto.Name) || string.IsNullOrEmpty(userDto.Email) || userDto.Password <= 0)
            {
                return new ApiResponse
                {
                    IsSuccess = false,
                    Message = "User cant be null",
                    Payload = null
                };
            }

            var user = m_Mapper.Map<User>(userDto);
          
            m_UserService.Register(user);

            return new ApiResponse
            {
                IsSuccess = true,
                Message = "User generated!",
                Payload = m_Mapper.Map<UserRegisterResponseDTO>(user)
            };
        }

        [HttpPost("Login")]
        public ApiResponse<UserLoginResponseDTO> Login(UserLoginRequestDTO userLoginDto)
        {
            var user = m_Mapper.Map<User>(userLoginDto);

            user = m_UserService.Login(user);

            if (user is null)
            {
                return new ApiResponse<UserLoginResponseDTO>
                {
                    IsSuccess = false,
                    Message = "User login Failed",
                    Payload = null
                };

            }

            return new ApiResponse<UserLoginResponseDTO>
            {
                IsSuccess = true,
                Message = "Success",
                Payload = m_Mapper.Map<UserLoginResponseDTO>(user)
            };
        }
    }
}
