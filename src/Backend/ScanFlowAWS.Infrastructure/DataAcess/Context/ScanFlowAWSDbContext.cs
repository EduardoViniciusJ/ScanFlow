using Microsoft.EntityFrameworkCore;
using ScanFlowAWS.Domain.Entities;

namespace ScanFlowAWS.Infrastructure.DataAcess.Context
{
    public class ScanFlowAWSDbContext : DbContext
    {
        public ScanFlowAWSDbContext(DbContextOptions<ScanFlowAWSDbContext> options) : base(options) { }
        public DbSet<User> DomainUsers { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
