using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;
using TaskManager.Application.ServicesContract;

namespace TaskManager.Application.Features.Tasks.Command.Handlers
{
    public class UpdateTaskCmdHandler:IRequestHandler<UpdateTaskCommand, TaskCmdResponseDto>
    {
        private readonly ITaskService _taskService;
        public UpdateTaskCmdHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<TaskCmdResponseDto> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            UpdateTaskDto updateTask = new UpdateTaskDto
            {
                Title = request.Title,
                Description = request.Description,
                IsCompleted = request.IsCompleted ?? false
            };
            var task = await _taskService.UpdateTask(request.Id, updateTask);
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
                CreatedAt = task.CreatedAt
            };
        }
    }
}
