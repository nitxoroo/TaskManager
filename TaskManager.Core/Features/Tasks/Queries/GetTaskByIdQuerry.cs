using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Features.Tasks.Queries
{
    public class GetTaskByIdQuerry:IRequest<TaskCmdResponseDto>
    {
        public Guid Id { get; set; }
    }
}
