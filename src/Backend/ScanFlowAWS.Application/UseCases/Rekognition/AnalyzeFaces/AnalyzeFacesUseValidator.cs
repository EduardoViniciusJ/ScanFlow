using FluentValidation;
using ScanFlowAWS.Application.DTOs.Requests;

namespace ScanFlowAWS.Application.UseCases.AmazonRekognition.AnalyzeFaces
{
    public class AnalyzeFacesUseValidator : AbstractValidator<RequestAnalyzeFacesJson>
    {
        public AnalyzeFacesUseValidator()
        {
            RuleFor(request => request.file).NotNull().WithMessage("É obrigatório enviar um arquivo de imagem no formato .png ou .jpg.");
        }

    }
}
