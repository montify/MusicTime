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
        public ActionResult<User> Register(UserRegisterDTO userDto)
        {
            if(userDto == null || string.IsNullOrEmpty(userDto.Name) || string.IsNullOrEmpty(userDto.Email))
            {
                return StatusCode(StatusCodes.Status204NoContent, "User cant be null");
            }

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Password = userDto.Password,
            };

            m_UserService.Register(user);

            return user;
        }
        [HttpPost("Login")]
        public ActionResult<User> Login(UserLoginDTO userLoginDto)
        {
            var user = m_UserService.Login(userLoginDto);

            if (user is not null)
            {
                //Generate JWT token
                return StatusCode(StatusCodes.Status200OK, user);
            }
            
            return StatusCode(StatusCodes.Status400BadRequest, null);
        }
    }
}
