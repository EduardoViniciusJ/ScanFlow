using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using ScanFlowAWS.Application.UseCases.User.Register;
using ScanFlowAWS.Application.UseCases.AmazonRekognition;
using ScanFlowAWS.Application.UseCases.AmazonRekognition.Interface;
using ScanFlowAWS.Application.UseCases.User.Register.Interfaces;


namespace ScanFlowAWS.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection service)
        {
            AddAcess(service);
            AddRekognition(service);    
        }

        public static void AddAcess(IServiceCollection service)
        {
            service.AddScoped<IRegisterUseCase, RegisterUseCase>();
        }

        public static void AddRekognition(IServiceCollection service)
        {
            service.AddScoped<IRekognitionUseCase, RekognitionUseCase>();   
        }

    }
}
