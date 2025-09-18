namespace ScanFlowAWS.Domain.Repositories.User
{
    public interface IUnitOfWork
    {
        public Task Commit();
    }
}
