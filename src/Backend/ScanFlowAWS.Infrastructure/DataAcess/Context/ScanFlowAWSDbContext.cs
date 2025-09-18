using Microsoft.EntityFrameworkCore;
using ScanFlowAWS.Domain.Entities;
using System.Reflection.Emit;

namespace ScanFlowAWS.Infrastructure.DataAcess.Context
{
    public class ScanFlowAWSDbContext : DbContext
    {
        public ScanFlowAWSDbContext(DbContextOptions<ScanFlowAWSDbContext> options) : base(options) { }
        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
                entity.Property(u => u.PasswordHash).IsRequired();
            });

            builder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshTokens");
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Token).IsRequired();
                entity.HasOne<User>() 
                      .WithMany()
                      .HasForeignKey(r => r.UserId);
            });

        }
    }
}
