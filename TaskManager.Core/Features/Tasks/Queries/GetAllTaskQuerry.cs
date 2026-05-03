using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Features.Tasks.Queries
{
    public class GetAllTaskQuerry:IRequest<List<TaskCmdResponseDto>>
    {
    }
}
