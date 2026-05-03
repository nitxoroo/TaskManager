using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.Features.Auth.Commands
{
    public class RefreshTokenCommand:IRequest<AuthResponseDto>  
    {
        public string RefreshToken { get; set; }
    }
}
