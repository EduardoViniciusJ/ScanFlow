using ScanFlowAWS.Web.Models;
using ScanFlowAWS.Web.Models.Responses;
using System.Net.Http;

namespace ScanFlowAWS.Web.Services.Interfaces
{
    public interface ILoginService
    {
        Task<ResponseLoginJson?> LoginAsync(LoginFormModel loginForm);
        Task<bool> RefreshTokenAsync(string accessToken, string refreshToken);
    }
}
