using ScanFlowAWS.Web.Models;

namespace ScanFlowAWS.Web.Services
{
    public interface IAuthService
    {
        Task<LoginResponseModel> LoginAsync(LoginRequestModel loginRequest);
    }
}
