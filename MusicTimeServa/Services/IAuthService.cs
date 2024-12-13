using Microsoft.AspNetCore.Identity.Data;
using MusicTimeServa.Model.DTOs.Response;

namespace MusicTimeServa.Services
{
    public interface IAuthService
    {
        public Task<ApiResponse> Register(RegisterRequest registerRequest);
        public Task<ApiResponse<UserLoginResponseDTO>> Login(LoginRequest loginRequest);
    }
}
