using Microsoft.Extensions.DependencyInjection;
using ScanFlowAWS.Application.UseCases.User.Register;
using ScanFlowAWS.Application.UseCases.AmazonRekognition;
using ScanFlowAWS.Application.UseCases.User.Register.Interfaces;
using ScanFlowAWS.Application.UseCases.AmazonRekognition.AnalyzeFaces.Interfaces;
using ScanFlowAWS.Application.UseCases.Rekognition.CompareceFaces.Interface;
using ScanFlowAWS.Application.UseCases.Rekognition.CompareceFaces;
using ScanFlowAWS.Application.UseCases.User.Login.Interfaces;
using ScanFlowAWS.Application.UseCases.User.Login;
using ScanFlowAWS.Application.UseCases.User.Token.Interfaces;
using ScanFlowAWS.Application.UseCases.User.Token;


namespace ScanFlowAWS.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection service)
        {
            AddToken(service);
            AddRegister(service);
            AddRekognition(service);
            AddLogin(service);
        }

        public static void AddRegister(IServiceCollection service)
        {
            service.AddScoped<IRegisterUseCase, RegisterUseCase>();
        }

        public static void AddLogin(IServiceCollection service)
        {
            service.AddScoped<ILoginUseCase, LoginUseCase>();
        }

        public static void AddRekognition(IServiceCollection service)
        {
            service.AddScoped<IAnalyzeFacesUseCase, AnalyzeFacesUseCase>(); 
            service.AddScoped<ICompareceFaces, CompareImagensUseCase>();    
        }

        public static void AddToken(IServiceCollection service)
        {
            service.AddScoped<ITokenUseCase, TokenUseCase>();    
        }

    }
}
