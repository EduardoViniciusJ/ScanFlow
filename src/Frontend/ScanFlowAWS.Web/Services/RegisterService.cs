using ScanFlowAWS.Web.Models;
using ScanFlowAWS.Web.Services.Interfaces;
using System.Net.Http.Json;

namespace ScanFlowAWS.Web.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly HttpClient _httpClient;

        public RegisterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterAsync(RegisterFormModel registerForm)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/register", registerForm);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(error);
            }
            return response.IsSuccessStatusCode;
        }
    }
}
