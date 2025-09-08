using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.DTOs.Responses;

namespace ScanFlowAWS.Application.UseCases.User.Register
{
    public interface IRegisterUseCase
    {
        Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);

    }
}
