using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;
using TaskManager.Application.Features.Auth.Commands;
using TaskManager.Application.ServicesContract;

namespace TaskManager.Application.Features.Auth.Handler
{
    public class RegisterCmdHandler : IRequestHandler<RegisterCommand, RegisterDto>
    {
        private readonly IAuthService _authService;
        public RegisterCmdHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RegisterDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var registerDto = new RegisterDto
            {
                UserName = request.UserName,
                Password = request.Password,
                Email = request.Email
            };
            var result = await _authService.Register(registerDto);
            return result;
        }
    }
}
