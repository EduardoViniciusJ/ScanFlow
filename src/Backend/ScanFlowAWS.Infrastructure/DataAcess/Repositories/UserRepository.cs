using Microsoft.EntityFrameworkCore;
using ScanFlowAWS.Domain.Entities;
using ScanFlowAWS.Domain.Repositories.User;
using ScanFlowAWS.Infrastructure.DataAcess.Context;

namespace ScanFlowAWS.Infrastructure.DataAcess.Repositories
{
    class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
    {
        private readonly ScanFlowAWSDbContext _context;

        public UserRepository(ScanFlowAWSDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));   
        }
    }
}
