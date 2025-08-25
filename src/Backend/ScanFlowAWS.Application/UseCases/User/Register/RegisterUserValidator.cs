using FluentValidation;
using ScanFlowAWS.Application.DTOs.Requests;

namespace ScanFlowAWS.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(register => register.Username).NotEmpty().WithMessage("O nome de usuário não pode ser vazio.");
            RuleFor(register => register.Email).NotEmpty().WithMessage("O e-mail não pode ser vazio.");
            RuleFor(register => register.Email).EmailAddress().WithMessage("O e-mail informado não é válido.");
            RuleFor(register => register.Password.Length).GreaterThanOrEqualTo(6).WithMessage("A senha deve conter pelo menos 6 caracteres.");
        }
    }
}
