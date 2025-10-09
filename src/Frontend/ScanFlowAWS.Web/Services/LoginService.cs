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


        public async Task<LoginResponseModel> LoginAsync(LoginRequestModel loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/user/login", loginRequest);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new ApplicationException(error);
            }

            var result = await response.Content.ReadFromJsonAsync<LoginResponseModel>();

            if(result == null)
            {
                throw new ApplicationException("Invalid response from server");
            }

            return result;
        }
    }
}
