namespace ScanFlowAWS.Domain.Repositories.User
{
    public interface IUserReadOnlyRepository
    {
        public Task<Entities.User?> GetByEmailAsync(string email);
        public Task<Entities.User?> GetByIdAsync(Guid id);
    }
}
