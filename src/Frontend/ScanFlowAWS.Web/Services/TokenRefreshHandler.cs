using System.Net;
using System.Net.Http.Headers;
using ScanFlowAWS.Web.Services.Interfaces;
using Microsoft.AspNetCore.Components;

public class TokenRefreshHandler : DelegatingHandler
{
    private readonly CustomAuthenticationStateProvider _authProvider;
    private readonly ILoginService _loginService;
    private readonly NavigationManager _navigation;

    public TokenRefreshHandler(
        CustomAuthenticationStateProvider authProvider,
        ILoginService loginService,
        NavigationManager navigation)
    {
        _authProvider = authProvider;
        _loginService = loginService;
        _navigation = navigation;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        // 🔹 Adiciona o token atual no cabeçalho da requisição
        var token = _authProvider.GetToken();
        if (!string.IsNullOrEmpty(token))
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        // 🔹 Executa a requisição
        var response = await base.SendAsync(request, cancellationToken);

        // 🔹 Se o token expirou (API retorna 401)
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var accessToken = _authProvider.GetToken();
            var refreshToken = _authProvider.GetRefreshToken();

            // 🔹 Se não há tokens, desloga direto
            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                _authProvider.Logout();
                _navigation.NavigateTo("/login", true);
                return response;
            }

            try
            {
                // 🔹 Tenta renovar o token
                var refreshed = await _loginService.RefreshTokenAsync(accessToken, refreshToken);

                if (refreshed)
                {
                    // 🔹 Novo token obtido — refaz a requisição original
                    var newToken = _authProvider.GetToken();
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);

                    response = await base.SendAsync(request, cancellationToken);
                }
                else
                {
                    // 🔹 Refresh token também expirou → logout
                    _authProvider.Logout();
                    _navigation.NavigateTo("/login", true);
                }
            }
            catch
            {
                // 🔹 Qualquer erro → desloga por segurança
                _authProvider.Logout();
                _navigation.NavigateTo("/login", true);
            }
        }

        return response;
    }
}
