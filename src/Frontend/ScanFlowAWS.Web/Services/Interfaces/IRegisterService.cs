using ScanFlowAWS.Web.Models;

namespace ScanFlowAWS.Web.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<bool> RegisterAsync(RegisterFormModel registerForm);
    }
}
