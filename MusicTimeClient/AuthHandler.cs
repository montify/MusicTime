using System.Net.Http.Headers;

namespace MusicTimeClient
{
    public class AuthHandler : DelegatingHandler
    {
        private readonly LocalStorage m_localStorage;

        public AuthHandler(LocalStorage localStorage)
        {
            m_localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var token = await m_localStorage.GetItem("jwtToken");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var send = await base.SendAsync(request, cancellationToken);
            return send;
        }
    }
}
