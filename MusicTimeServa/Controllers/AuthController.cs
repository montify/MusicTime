using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using MusicTimeServa.Model.DTOs.Response;
using MusicTimeServa.Services;

namespace MusicTimeServa.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AuthController : ControllerBase
    {
        private readonly IAuthService m_AuthService;

        public AuthController(IMapper mapper, IAuthService authService)
        {
            m_AuthService = authService;
        }

        [HttpPost("Register")]
        public async Task<ApiResponse> Register(RegisterRequest registerRequest)
        {
            return await m_AuthService.Register(registerRequest);
        }

        [HttpPost("Login")]
        public async Task<ApiResponse<UserLoginResponseDTO>> Login(LoginRequest loginRequest)
        {
            return await m_AuthService.Login(loginRequest);
        }
    }
}
