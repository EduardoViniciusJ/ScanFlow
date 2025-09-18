namespace ScanFlowAWS.Domain.Repositories.User
{
    public interface IUserWriteOnlyRepository
    {
        public Task AddAsync(Entities.User user);
    }
}
