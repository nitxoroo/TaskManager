using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Features.Tasks.Command
{
    public class UpdateTaskCommand:IRequest<TaskCmdResponseDto>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
