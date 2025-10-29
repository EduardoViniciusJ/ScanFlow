using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private string? _accessToken;
    private string? _refreshToken;

    public string? GetToken() => _accessToken;
    public string? GetRefreshToken() => _refreshToken;

    public void SetToken(string accessToken, string refreshToken)
    {
        _accessToken = accessToken;
        _refreshToken = refreshToken;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public void Logout()
    {
        _accessToken = null;
        _refreshToken = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (string.IsNullOrEmpty(_accessToken))
            return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "Usuário Logado")
        }, "jwtAuth");

        return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
    }
}
