using ScanFlowAWS.Domain.Entities;
using ScanFlowAWS.Domain.Repositories.User;
using ScanFlowAWS.Infrastructure.DataAcess.Context;

namespace ScanFlowAWS.Infrastructure.DataAcess.Repositories
{
    class UserRepository : IUserWriteOnlyRepository
    {
        private readonly ScanFlowAWSDbContext _context;

        UserRepository(ScanFlowAWSDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
    }
}
