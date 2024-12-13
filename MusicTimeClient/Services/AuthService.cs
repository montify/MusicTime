using Microsoft.AspNetCore.Components.Authorization;
using MusicTimeClient.Contracts;
using MusicTimeClient.Models;
using MusicTimeClient.Models.DTOs.Request;
using MusicTimeClient.Models.DTOs.Response;
using MusicTimeClient.Provider;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace MusicTimeClient.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient m_httpClient;
        private readonly LocalStorage m_localStorage;
        private readonly AuthenticationStateProvider m_AuthenticationStateProvider;

        public AuthService(HttpClient httpClient, LocalStorage localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            m_httpClient = httpClient;
            m_localStorage = localStorage;
            m_AuthenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> IsLoggedIn()
        {
            var authState = await m_AuthenticationStateProvider.GetAuthenticationStateAsync();

            return authState.User is not null;
            //return m_localStorage.ContainsKey("jwtToken");
        }

        public async Task<User> Login(UserLoginRequestDTO userRequest)
        {
            var response = await m_httpClient.PostAsJsonAsync<UserLoginRequestDTO>("Auth/Login", userRequest);
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse<UserLoginResponseDTO>>();

            if (!apiResponse.IsSuccess)
                return null;

            var user = new User
            {
                Email = apiResponse.Payload.Email,
                Token = apiResponse.Payload.Token
            };

            //STORE JWT
            await m_localStorage.AddItem("jwtToken", user.Token);
            await ((ApiAuthenticationStateProvider)m_AuthenticationStateProvider).LoggedIn();

            return user;
        }

        public async Task Logout()
        {
            await m_localStorage.RemoveItem("jwtToken");
            ((ApiAuthenticationStateProvider)m_AuthenticationStateProvider).LoggedOut();
        }

        public async Task<ApiResponse> Register(UserRegisterRequestDTO userRequest)
        {
            var response = await m_httpClient.PostAsJsonAsync("Auth/Register", userRequest);
            var apiResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();

            if (!apiResponse.IsSuccess)
                return new ApiResponse { IsSuccess = false, Message = "Register failed!" };

            return new ApiResponse { IsSuccess = true, Message = "Successfully Registered" };
        }
    }
}