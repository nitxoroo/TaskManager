using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Features.Tasks.Command
{
    public class DeleteTaskCommand:IRequest<TaskCmdResponseDto>
    {
        public Guid Id { get; set; }
    }
}
