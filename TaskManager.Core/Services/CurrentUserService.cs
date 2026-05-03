using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using TaskManager.Application.ServicesContract;

namespace TaskManager.Application.Services
{
    public class CurrentUserService:ICurrentUserService
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid UserId =>
            Guid.Parse(
                _httpContextAccessor.HttpContext!
                .User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );
    }
}
