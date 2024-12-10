using MusicTimeClient.Contracts;
using MusicTimeClient.Models;
using MusicTimeClient.Models.DTOs.Request;
using MusicTimeClient.Models.DTOs.Response;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace MusicTimeClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient m_httpClient;

        public AuthService(HttpClient httpClient)
        {
            m_httpClient = httpClient;
        }

        public async Task<User> Login(UserLoginRequestDTO userRequest)
        {
            var response = await m_httpClient.PostAsJsonAsync<UserLoginRequestDTO>("Auth/Login", userRequest);
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<UserLoginResponseDTO>>();
  
            if(!apiResponse.IsSuccess)
                return null;

            var user = new User
            {
                Email = apiResponse.Payload.Email,
                Token = apiResponse.Payload.Token
            };

            return user;
        }

        public async Task<ApiResponse> Register(UserRegisterRequestDTO userRequest)
        {
            var response = await m_httpClient.PostAsJsonAsync("Auth/Register", userRequest);
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<UserLoginResponseDTO>>();

            if (!apiResponse.IsSuccess)
                return new ApiResponse { IsSuccess = false, Message = "Register failed!" };

            return new ApiResponse { IsSuccess = true, Message = "Successfully Registered" };

        }

       
    }
}
