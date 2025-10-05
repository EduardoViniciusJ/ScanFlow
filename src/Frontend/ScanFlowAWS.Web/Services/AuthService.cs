using ScanFlowAWS.Web.Models;
using System.Net.Http.Json;

namespace ScanFlowAWS.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<LoginResponseModel> LoginAsync(LoginRequestModel loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginRequest);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(error);
            }

            return await response.Content.ReadFromJsonAsync<LoginResponseModel>();

        }
    }
}
