using FluentValidation;
using ScanFlowAWS.Application.DTOs.Requests;

namespace ScanFlowAWS.Application.UseCases.AmazonRekognition
{
    public class RekognitionUseValidator : AbstractValidator<RequestRekognition>
    {
        public RekognitionUseValidator()
        {
            RuleFor(rekognition => rekognition.file).NotEmpty().NotNull().WithMessage("Você precisa colocar uma imagem.");
        }

    }
}
