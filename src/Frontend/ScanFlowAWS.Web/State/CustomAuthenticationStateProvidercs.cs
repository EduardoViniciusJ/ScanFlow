using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Components.Authorization;

namespace ScanFlowAWS.Web.State
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private string? _token;
        private readonly ClaimsPrincipal _anonymous = new(new ClaimsIdentity());

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (string.IsNullOrWhiteSpace(_token))
                return Task.FromResult(new AuthenticationState(_anonymous));

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(_token);

                // define um tipo de autenticação para o identity
                var identity = new ClaimsIdentity(jwt.Claims, authenticationType: "jwt");

                // cria o usuário com base nas claims do token
                var user = new ClaimsPrincipal(identity);

                return Task.FromResult(new AuthenticationState(user));
            }
            catch
            {
                // se der erro ao ler o token, considera usuário anônimo
                return Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public void SetToken(string token)
        {
            _token = token;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public void Logout()
        {
            _token = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public string? GetToken() => _token;
    }
}
