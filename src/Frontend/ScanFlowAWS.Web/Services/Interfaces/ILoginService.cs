using ScanFlowAWS.Web.Models;
using System.Net.Http;

namespace ScanFlowAWS.Web.Services.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponse?> LoginAsync(LoginFormModel loginForm);
        Task<bool> RefreshTokenAsync(string accessToken, string refreshToken);
    }
}
