using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Identities;

namespace TaskManager.Application.Features.Users.Queries.Handler
{
    public class GetAllUserHandler:IRequestHandler<GetAllUserQuery,List<UserDto>>
    {
        private readonly UserManager<User> _userManager;
        
        public GetAllUserHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<UserDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken) 
        {
            var users = _userManager.Users.ToList();

            return users.Select(u => new UserDto
            {
                Id = u.Id.ToString(),
                Email = u.Email,
                UserName = u.UserName
            }).ToList();
        }
    }
}
