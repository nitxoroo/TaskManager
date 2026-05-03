using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;
using TaskManager.Application.RepoContract;
using TaskManager.Application.ServicesContract;

namespace TaskManager.Application.Features.Tasks.Command.Handlers
{
    public class DeleteTaskCmdHandler:IRequestHandler<DeleteTaskCommand, TaskCmdResponseDto>
    {
        private readonly ITaskService _taskService;
        public DeleteTaskCmdHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<TaskCmdResponseDto> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await _taskService.DeleteTask(request.Id);
            if (task == null)
            {
                return null;
            }
            await _taskService.DeleteTask(task.Id   );
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
