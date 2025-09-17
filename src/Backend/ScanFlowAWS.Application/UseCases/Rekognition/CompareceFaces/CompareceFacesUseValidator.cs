using FluentValidation;
using ScanFlowAWS.Application.DTOs.Requests.Rekognition;

namespace ScanFlowAWS.Application.UseCases.Rekognition.CompareceFaces
{
    public class CompareceFacesUseValidator : AbstractValidator<RequestCompareceFacesJson>
    {
        public CompareceFacesUseValidator()
        {
            RuleFor(request => request.FileSource).NotNull().WithMessage("Por favor, envie a primeira imagem para a análise.");
            RuleFor(request => request.FileTarget).NotNull().WithMessage("Por favor, envie a segunda imagem para a análise.");
        }

    }
}
