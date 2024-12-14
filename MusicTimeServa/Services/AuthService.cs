using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;
using MusicTimeServa.Model;
using MusicTimeServa.Model.DTOs.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicTimeServa.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration m_Configuration;
        private readonly UserManager<User> m_UserManager;
        private readonly SignInManager<User> m_SignInManager;

        public AuthService(IConfiguration configuration, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            m_Configuration = configuration;
            m_UserManager = userManager;
            m_SignInManager = signInManager;
        }

        public async Task<ApiResponse<UserLoginResponseDTO>> Login(LoginRequest loginRequest)
        {
            var response = new ApiResponse<UserLoginResponseDTO>();
            response.Payload = new UserLoginResponseDTO();
            var user = await m_UserManager.FindByEmailAsync(loginRequest.Email);

            if (user == null)
            {
                response.IsSuccess = false;
                response.Message = "Cant find user";

                return response;
            }

            var result = await m_UserManager.CheckPasswordAsync(user, loginRequest.Password.ToString());

            if (result is false)
            {
                response.IsSuccess = false;
                response.Message = $"Wrong password for User: {user.UserName}";

                return response;
            }

            //Log in
            var token = GenerateJwtToken(user);
            var loginResult = await m_SignInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, false, lockoutOnFailure: true);

            if (loginResult.Succeeded)
            {
                response.Message = "Successfully logged in";
                response.IsSuccess = true;
                response.Payload.Token = new JwtSecurityTokenHandler().WriteToken(token);
                response.Payload.Email = loginRequest.Email;

                return response;
            }

            response.Message = $"ERROR result: {loginResult}";
            response.IsSuccess = false;
            response.Payload.Token = string.Empty;
            response.Payload.Email = loginRequest.Email;

            return response;
        }

        public async Task<ApiResponse> Register(RegisterRequest registerRequest)
        {
            ApiResponse response = new ApiResponse();

            var user = new User
            {
                UserName = registerRequest.Email,
                Email = registerRequest.Email,
            };

            var result = await m_UserManager.CreateAsync(user, registerRequest.Password);

            if (!result.Succeeded)
            {
                response.IsSuccess = false;
                response.Message = "Cant register User";
                response.Payload = result.Errors.ToArray();
                return response;

            }
            response.IsSuccess = true;
            response.Message = "User registerd successfully!";
            response.Payload = string.Empty;
            return response;
        }

        private SecurityToken GenerateJwtToken(User user)
        {
            var issuer = m_Configuration.GetSection("Jwt:Issuer").Get<string>();
            var durationInMinute = m_Configuration.GetSection("Jwt:DurationInMinutes").Get<int>();

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(m_Configuration["Jwt:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Email, user.Email),
               new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
               new Claim(JwtRegisteredClaimNames.Iss, m_Configuration["Jwt:Issuer"])
           };

            var jwtSecurityToken = new JwtSecurityToken(
               issuer: issuer,
               audience: m_Configuration["Jwt:Audience"],
               claims: claims,
               expires: DateTime.UtcNow.Add(new TimeSpan(0, 2, 0, 0, 0, 0)), //TODO: Figure out why its lag behin 1 hour, from wintertime, so i need to add 2 hours..
               signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}

