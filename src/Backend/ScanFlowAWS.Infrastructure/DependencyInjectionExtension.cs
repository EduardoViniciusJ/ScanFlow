using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScanFlowAWS.Domain.Services;
using ScanFlowAWS.Infrastructure.Adapters;
using ScanFlowAWS.Infrastructure.DataAcess.Context;
using ScanFlowAWS.Infrastructure.Services;

namespace ScanFlowAWS.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection service, IConfiguration configuration)
        {
            AddDbContextSqlServer(service, configuration);
            AddRekognitionService(service, configuration);
            AddRekognitionAdapter(service);
        }


        public static void AddDbContextSqlServer(IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<ScanFlowAWSDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnectionString"),
                    b => b.MigrationsAssembly("ScanFlowAWS.Infrastructure")
                );
            });
        }

        public static void AddRekognitionService(IServiceCollection service, IConfiguration configuration)
        {
            var region = configuration["AWS:Region"] ?? "us-east-1";
            service.AddSingleton(new RekognitionService(region));
        }

        public static void AddRekognitionAdapter(IServiceCollection service)
        {
            service.AddScoped<IRekognitionService, RekognitionAdapter>();   
        }
    }
}
