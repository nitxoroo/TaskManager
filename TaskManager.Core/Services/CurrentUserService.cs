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

        public Guid? UserId
        {
            get
            {
                var user = _httpContextAccessor.HttpContext?.User;

                if (user == null || !user.Identity!.IsAuthenticated)
                    return null;

                var claim = user.FindFirst(ClaimTypes.NameIdentifier);

                if (claim == null)
                    return null;

                return Guid.Parse(claim.Value);
            }
        }
    }
}
