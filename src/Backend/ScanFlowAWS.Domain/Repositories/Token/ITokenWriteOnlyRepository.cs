using ScanFlowAWS.Domain.Entities;

namespace ScanFlowAWS.Domain.Repositories.Token
{
    public interface ITokenWriteOnlyRepository
    {
        Task AddAsync(Entities.Token token);
        void Delete(Entities.Token token);
    }
}
