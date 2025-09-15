using FluentValidation;
using ScanFlowAWS.Application.DTOs.Requests;

namespace ScanFlowAWS.Application.UseCases.AmazonRekognition
{
    public class RekognitionUseValidator : AbstractValidator<RequestRekognitionJson>
    {
        public RekognitionUseValidator()
        {
            RuleFor(rekognition => rekognition.file).NotNull().WithMessage("É obrigatório enviar um arquivo de imagem no formato .png ou .jpg.");
        }

    }
}
