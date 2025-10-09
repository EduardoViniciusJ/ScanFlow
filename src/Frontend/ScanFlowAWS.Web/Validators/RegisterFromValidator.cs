﻿using FluentValidation;
using ScanFlowAWS.Web.Models;

namespace ScanFlowAWS.Web.Validators
{
    public class RegisterFormValidator : AbstractValidator<RegisterFormModel>
    {
        public RegisterFormValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Passwords do not match");
        }
    }
}
