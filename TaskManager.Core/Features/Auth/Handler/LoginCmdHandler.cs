using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;
using TaskManager.Application.Features.Auth.Commands;
using TaskManager.Application.ServicesContract;

namespace TaskManager.Application.Features.Auth.Handler
{
    public class LoginCmdHandler:IRequestHandler<LoginCommand, AuthResponseDto>
    {
        private readonly IAuthService _authService;
        public LoginCmdHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var loginDto = new LoginDto
            {
                Email = request.Email,
                Password = request.Password
            };
            var authResponse = await _authService.Login(loginDto);
            if (authResponse == null)
            {
                return null;
            }

            return authResponse;
        }
    }
}
