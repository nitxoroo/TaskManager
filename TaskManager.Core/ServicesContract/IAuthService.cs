using Microsoft.AspNetCore.Identity.Data;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;

namespace TaskManager.Application.ServicesContract
{
    public interface IAuthService
    {
        Task<RegisterDto> Register(RegisterDto dto);
        Task<AuthResponseDto> Login(LoginDto dto);
        Task<AuthResponseDto> RefreshToken(string refreshToken);

    }
}
