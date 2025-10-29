using System.Net.Http.Json;
using ScanFlowAWS.Web.Models;
using ScanFlowAWS.Web.Models.Responses;
using ScanFlowAWS.Web.Services.Interfaces;

public class LoginService : ILoginService
{
    private readonly HttpClient _http;
    private readonly CustomAuthenticationStateProvider _authProvider;

    public LoginService(HttpClient http, CustomAuthenticationStateProvider authProvider)
    {
        _http = http;
        _authProvider = authProvider;
    }

    public async Task<LoginResponse?> LoginAsync(LoginFormModel loginForm)
    {
        var response = await _http.PostAsJsonAsync("api/user/login", loginForm);
        if (!response.IsSuccessStatusCode) return null;

        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
        if (loginResponse != null)
            _authProvider.SetToken(loginResponse.AccessToken, loginResponse.RefreshToken);

        return loginResponse;
    }

    public async Task<bool> RefreshTokenAsync(string accessToken, string refreshToken)
    {
        var body = new { AccessToken = accessToken, RefreshToken = refreshToken };
        var response = await _http.PostAsJsonAsync("api/user/refresh-token", body);

        if (!response.IsSuccessStatusCode)
            return false;

        var tokens = await response.Content.ReadFromJsonAsync<LoginResponse>();
        if (tokens == null || string.IsNullOrEmpty(tokens.AccessToken))
            return false;

        _authProvider.SetToken(tokens.AccessToken, tokens.RefreshToken);
        return true;
    }
}
