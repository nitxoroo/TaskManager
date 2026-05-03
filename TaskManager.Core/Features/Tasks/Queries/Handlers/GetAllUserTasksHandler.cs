using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.ServicesContract;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Features.Tasks.Queries.Handlers
{
    public class GetAllUserTasksHandler:IRequestHandler<GetAllUserTaskQuery,List<TaskItem>>
    {
        private readonly ITaskService _taskService;

        public GetAllUserTasksHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<List<TaskItem>> Handle (GetAllUserTaskQuery request, CancellationToken token)
        {
            return await _taskService.GetAllUserTask();
        }
    }
}
