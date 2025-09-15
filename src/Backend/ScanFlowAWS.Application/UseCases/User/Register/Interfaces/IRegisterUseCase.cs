using ScanFlowAWS.Application.DTOs.Requests.User;
using ScanFlowAWS.Application.DTOs.Responses.User;

namespace ScanFlowAWS.Application.UseCases.User.Register.Interfaces
{
    public interface IRegisterUseCase
    {
        Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);

    }
}
