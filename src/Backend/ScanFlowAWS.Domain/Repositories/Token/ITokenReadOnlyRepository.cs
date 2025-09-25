using ScanFlowAWS.Domain.Entities;

namespace ScanFlowAWS.Domain.Repositories.Token
{
    public interface ITokenReadOnlyRepository
    {
        Task<Entities.Token> GetByTokenAsync(string token);
    }
}
