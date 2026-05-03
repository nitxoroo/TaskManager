using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;
using TaskManager.Application.Features.Auth.Commands;
using TaskManager.Application.ServicesContract;

namespace TaskManager.Application.Features.Auth.Handler
{
    public class RefreshTokenCmdHandler:IRequestHandler<RefreshTokenCommand, AuthResponseDto>
    {
        private readonly IAuthService _authService;
        public RefreshTokenCmdHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<AuthResponseDto> Handle (RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var result = await _authService.RefreshToken(command.RefreshToken);
            return result;
        }
    }
}
