using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.Features.Tasks.Command;

namespace TaskManager.Application.Features.Auth.Validators
{
    public class AddTaskValidator:AbstractValidator<AddTaskCommand>
    {
        public AddTaskValidator()
        {
            
        }
    }
}
