using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Features.Users.Queries
{
    public class GetAllUserQuery:IRequest<List<UserDto>>
    {
    }
}
