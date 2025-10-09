using ScanFlowAWS.Web.Models;

namespace ScanFlowAWS.Web.Services.Interfaces
{
    public interface ILoginService
    {
        Task<LoginResponseModel> LoginAsync(LoginRequestModel loginRequest);
    }
}
