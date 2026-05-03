using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;
using TaskManager.Application.RepoContract;
using TaskManager.Application.ServicesContract;

namespace TaskManager.Application.Features.Tasks.Command.Handlers
{
    public class AddTaskCmdHandler:IRequestHandler<AddTaskCommand, TaskCmdResponseDto>
    {
        private readonly ITaskService _taskService;
        public AddTaskCmdHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<TaskCmdResponseDto> Handle(AddTaskCommand request, CancellationToken cancellationToken)
        {
            AddTaskDto addTask = new AddTaskDto
            {
                Title = request.Title,
                Description = request.Description
                
            };

            var Task = await _taskService.AddTask(addTask);
            return new TaskCmdResponseDto
            {
                Id = Task.Id,
                Title = Task.Title,
                Description = Task.Description,
                IsCompleted = Task.IsCompleted,
                CreatedAt = Task.CreatedAt
            };
        }
    }
}
