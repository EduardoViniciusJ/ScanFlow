using Microsoft.EntityFrameworkCore;
using ScanFlowAWS.Domain.Entities;

namespace ScanFlowAWS.Infrastructure.DataAcess.Context
{
    public class ScanFlowAWSDbContext : DbContext
    {
        public ScanFlowAWSDbContext(DbContextOptions<ScanFlowAWSDbContext> options) : base(options) { }
        public DbSet<User> DomainUsers { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Emotion> Emotions { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(u => u.Photos)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Photo>()
                .HasMany(p => p.Emotions)
                .WithOne(e => e.Photo)
                .HasForeignKey(e => e.PhotoId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.Entity<User>()
                .HasMany(u => u.Emotions)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
