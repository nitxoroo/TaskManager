using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;
using TaskManager.Application.ServicesContract;

namespace TaskManager.Application.Features.Tasks.Queries.Handlers
{
    public class GetAllTaskQueryHandler:IRequestHandler<GetAllTaskQuerry,List<TaskCmdResponseDto>>
    {
        private readonly ITaskService _taskService;
        public GetAllTaskQueryHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<List<TaskCmdResponseDto>> Handle(GetAllTaskQuerry request, CancellationToken cancellationToken)
        {
            var tasks = await _taskService.GetAllTasks();
            List<TaskCmdResponseDto> taskDtos = new List<TaskCmdResponseDto>();
            foreach (var task in tasks)
            {
                taskDtos.Add(new TaskCmdResponseDto
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    IsCompleted = task.IsCompleted,
                    CreatedAt = task.CreatedAt
                });
            }
            return taskDtos;
        }
    }
}
