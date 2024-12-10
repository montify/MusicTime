using MusicTimeClient.Models;
using MusicTimeClient.Models.DTOs.Request;

namespace MusicTimeClient.Contracts
{
    public interface IAuthService
    {
        public Task<ApiResponse> Register(UserRegisterRequestDTO user);
        public Task<User> Login(UserLoginRequestDTO userRequest);
        public Task Logout();
        public Task<bool> IsLoggedIn();
    }
}
