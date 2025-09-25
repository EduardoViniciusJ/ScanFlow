using ScanFlowAWS.Application.DTOs.Requests.Token;
using ScanFlowAWS.Application.DTOs.Requests.User;
using ScanFlowAWS.Application.DTOs.Responses.User;

namespace ScanFlowAWS.Application.UseCases.User.Token.Interfaces
{
    public interface IRefreshTokenUseCase
    {
        Task<ResponseLoginUserJson> Execute(RequestTokenJson request);
    }
}
