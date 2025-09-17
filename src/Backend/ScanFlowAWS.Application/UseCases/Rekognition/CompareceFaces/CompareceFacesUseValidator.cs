using FluentValidation;
using ScanFlowAWS.Application.DTOs.Requests.Rekognition;
using ScanFlowAWS.Application.Exceptions.ResourcesMassages;

namespace ScanFlowAWS.Application.UseCases.Rekognition.CompareceFaces
{
    public class CompareceFacesUseValidator : AbstractValidator<RequestCompareceFacesJson>
    {
        public CompareceFacesUseValidator()
        {
            RuleFor(request => request.FileSource).NotNull().WithMessage(ResourceMessageException.FIRST_IMAGE_EMPTY);
            RuleFor(request => request.FileTarget).NotNull().WithMessage(ResourceMessageException.SECOND_IMAGE_EMPTY);
        }

    }
}
