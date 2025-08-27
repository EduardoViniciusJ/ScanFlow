using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ScanFlowAWS.Application.UseCases.User.Register;


namespace ScanFlowAWS.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection service, IConfiguration configuration)
        {

        }

        public static void AddUseCases(IServiceCollection service)
        {
            service.AddScoped<IRegisterUseCase, RegisterUseCase>(); 
        }
    }
}
