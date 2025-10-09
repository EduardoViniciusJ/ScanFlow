using ScanFlowAWS.Web.Models;

namespace ScanFlowAWS.Web.Services.Interfaces
{
    public interface ILoginService
    {
        Task<bool> LoginAsync(LoginFormModel loginForm);
    }
}
