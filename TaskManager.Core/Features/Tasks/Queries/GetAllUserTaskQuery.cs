using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Features.Tasks.Queries
{
    public class GetAllUserTaskQuery:IRequest<List<TaskItem>>
    {
    }
}
