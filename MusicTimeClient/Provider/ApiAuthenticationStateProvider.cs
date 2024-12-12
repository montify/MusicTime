using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MusicTimeClient.Provider
{
    public class ApiAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient m_httpClient;
        private readonly LocalStorage m_localSotrage;
        private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;

        public ApiAuthenticationStateProvider(HttpClient httpClient, LocalStorage localStorage)
        {
            m_httpClient = httpClient;
            m_localSotrage = localStorage;
            jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await m_localSotrage.GetItem("jwtToken");
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());

            if (string.IsNullOrEmpty(token))
            {
                //empty ClaimsPrincipal means no permission to loggin
               new ClaimsPrincipal(new ClaimsIdentity());
                return new AuthenticationState(nobody);
            }

            var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(token);
         
            if (tokenContent.ValidTo < DateTime.Now)
            {
                await m_localSotrage.RemoveItem("jwtToken");
                return new AuthenticationState(nobody);
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "a")
            };
            
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
        }

        public async Task LoggedIn()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "TEESTName")
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(user));
            NotifyAuthenticationStateChanged(authState);
        }

        public void LoggedOut()
        {
            var nobody = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(nobody));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
