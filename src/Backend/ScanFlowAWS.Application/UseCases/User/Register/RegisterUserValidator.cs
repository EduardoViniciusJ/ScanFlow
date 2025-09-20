using FluentValidation;
using ScanFlowAWS.Application.DTOs.Requests.User;
using ScanFlowAWS.Application.Exceptions.ResourcesMassages;

namespace ScanFlowAWS.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(register => register.Username).NotEmpty().WithMessage(ResourceMessageException.USERNAME_EMPTY);
            RuleFor(register => register.Email).NotEmpty().WithMessage(ResourceMessageException.EMAIL_EMPTY);
            RuleFor(register => register.Email).EmailAddress().WithMessage(ResourceMessageException.EMAIL_INVALID);
            RuleFor(register => register.Password).MinimumLength(6).WithMessage(ResourceMessageException.PASSWORD_INVALID);
            RuleFor(register => register.Password).NotEmpty().WithMessage(ResourceMessageException.PASSWORD_EMPTY);
        }
    }
}
