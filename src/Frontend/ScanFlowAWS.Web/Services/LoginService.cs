using ScanFlowAWS.Web.Models;
using ScanFlowAWS.Web.Services.Interfaces;
using System.Net.Http.Json;

namespace ScanFlowAWS.Web.Services
{
    public class LoginService : ILoginService
    {
        private readonly HttpClient _httpClient;

        public LoginService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string?> LoginAsync(LoginFormModel loginForm)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/login", loginForm);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException($"Erro ao fazer login: {error}");
            }

            // Lê o JSON que vem da API
            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            return result?.AccessToken;
        }

    }
}
