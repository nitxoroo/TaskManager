using FluentValidation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using TaskManager.Application.Features.Auth.Commands;

namespace TaskManager.Application.Features.Auth.Validators
{
    public class LoginValidator:AbstractValidator<LoginCommand>
    {
        public LoginValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(5);
        }
    }
}
