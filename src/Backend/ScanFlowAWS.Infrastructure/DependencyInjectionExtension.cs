using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScanFlowAWS.Infrastructure.DataAcess.Context;

namespace ScanFlowAWS.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContextSqlServer(services, configuration);
        }

        public static void AddDbContextSqlServer(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ScanFlowAWSDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("ScanFlowAWS.Infrastructure") 
                );
            });
        }

        public static void AddIdentity(IServiceCollection services, IConfiguration configuration)
        {
            
        }
    }
}
