﻿using FluentValidation;

namespace ScanFlowAWS.Web.Validators
{
    public class LoginFormValidator : AbstractValidator<LoginFormModel> 
    {
        public LoginFormValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage("Email is required")
               .EmailAddress()
               .WithMessage("Invalid email format")
               .MaximumLength(255)
               .WithMessage("Email must not exceed 255 characters");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters")
                .MaximumLength(100)
                .WithMessage("Password must not exceed 100 characters");
        }
    }
}
