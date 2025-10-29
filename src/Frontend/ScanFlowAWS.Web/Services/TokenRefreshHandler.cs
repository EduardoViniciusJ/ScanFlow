using System.Net;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using ScanFlowAWS.Web.Services.Interfaces;

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
        var token = _authProvider.GetToken();

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var response = await base.SendAsync(request, cancellationToken);

        // 🔹 token expirado → tenta renovar
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var accessToken = _authProvider.GetToken();
            var refreshToken = _authProvider.GetRefreshToken();

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
            {
                _authProvider.Logout();
                _navigation.NavigateTo("/login", true);
                return response;
            }

            try
            {
                var refreshed = await _loginService.RefreshTokenAsync(accessToken, refreshToken);

                if (refreshed)
                {
                    var newToken = _authProvider.GetToken();
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
                    response = await base.SendAsync(request, cancellationToken);
                }
                else
                {
                    _authProvider.Logout();
                    _navigation.NavigateTo("/login", true);
                }
            }
            catch
            {
                _authProvider.Logout();
                _navigation.NavigateTo("/login", true);
            }
        }

        return response;
    }
}
