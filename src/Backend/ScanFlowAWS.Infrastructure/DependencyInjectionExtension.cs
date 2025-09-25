using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ScanFlowAWS.Domain.Repositories.Token;
using ScanFlowAWS.Domain.Repositories.User;
using ScanFlowAWS.Domain.Security;
using ScanFlowAWS.Domain.Services;
using ScanFlowAWS.Infrastructure.Adapters;
using ScanFlowAWS.Infrastructure.DataAcess;
using ScanFlowAWS.Infrastructure.DataAcess.Context;
using ScanFlowAWS.Infrastructure.DataAcess.Repositories;
using ScanFlowAWS.Infrastructure.Security;
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
            AddTranslatorJsonAdapter(service);
            AddUnitOfWork(service);
            AddEncripter(service);
            AddRepositories(service);
            AddTokenService(service);
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

        public static void AddTranslatorJsonAdapter(IServiceCollection service)
        {
            service.AddScoped<ITranslatorJsonService, TranslatorJsonService>();
        }
        public static void AddUnitOfWork(IServiceCollection service)
        {
            service.AddScoped<IUnitOfWork, UnitOfWork>();
        }
        public static void AddEncripter(IServiceCollection service)
        {
            service.AddScoped<IPasswordEncripter, BCryptNet>();
        }

        public static void AddRepositories(IServiceCollection service)
        {
            service.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            service.AddScoped<IUserReadOnlyRepository, UserRepository>();
            service.AddScoped<ITokenWriteOnlyRepository, TokenRepository>();
            service.AddScoped<ITokenReadOnlyRepository, TokenRepository>();
        }

        public static void AddTokenService(IServiceCollection service)
        {
            service.AddScoped<ITokenService,  TokenService>();  
        }
    }
}
