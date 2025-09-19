using ScanFlowAWS.Application.DTOs.Requests.User;
using ScanFlowAWS.Application.DTOs.Responses.User;

namespace ScanFlowAWS.Application.UseCases.User.Login.Interfaces
{
    public interface ILoginUseCase
    {
        Task<ResponseLoginUserJson> Execute(RequestLoginUserJson request);
    }
}
