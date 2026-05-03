using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;
using TaskManager.Application.ServicesContract;

namespace TaskManager.Application.Features.Tasks.Queries.Handlers
{
    public class GetTaskByIdQuerryHandler : IRequestHandler<GetTaskByIdQuerry, TaskCmdResponseDto>
    {
        private readonly ITaskService _taskService;
        public GetTaskByIdQuerryHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<TaskCmdResponseDto> Handle(GetTaskByIdQuerry request, CancellationToken cancellationToken)
        {
            var task = await _taskService.GetTaskById(request.Id);
            if (task == null)
            {
                return null;
            }
            return new TaskCmdResponseDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted,
                CreatedAt = task.CreatedAt,
            };

        }
    }
}
