using FluentValidation;
using ScanFlowAWS.Application.DTOs.Requests;
using ScanFlowAWS.Application.Exceptions.ResourcesMassages;

namespace ScanFlowAWS.Application.UseCases.AmazonRekognition.AnalyzeFaces
{
    public class AnalyzeFacesUseValidator : AbstractValidator<RequestAnalyzeFacesJson>
    {
        public AnalyzeFacesUseValidator()
        {
            RuleFor(request => request.file).NotNull().WithMessage(ResourceMessageException.IMAGE_EMPTY);
        }

    }
}
