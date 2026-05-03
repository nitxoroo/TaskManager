using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Features.Tasks.Command
{
    public class AddTaskCommand:IRequest<TaskCmdResponseDto>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
    }
}
