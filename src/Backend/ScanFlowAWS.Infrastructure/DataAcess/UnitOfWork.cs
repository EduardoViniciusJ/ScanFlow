using Microsoft.EntityFrameworkCore;
using ScanFlowAWS.Domain.Repositories.User;
using ScanFlowAWS.Infrastructure.DataAcess.Context;

namespace ScanFlowAWS.Infrastructure.DataAcess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ScanFlowAWSDbContext _context;
        public UnitOfWork(ScanFlowAWSDbContext context)
        {
            _context = context;
        }
        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
