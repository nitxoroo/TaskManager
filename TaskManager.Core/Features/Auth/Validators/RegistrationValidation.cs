using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.Features.Auth.Commands;

namespace TaskManager.Application.Features.Auth.Validators
{
    public class RegistrationValidation:AbstractValidator<RegisterCommand>
    {
        public RegistrationValidation() {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8);
        }
    }
}
