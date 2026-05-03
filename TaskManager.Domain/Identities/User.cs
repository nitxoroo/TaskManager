using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Identities
{
    public class User: IdentityUser<Guid>
    {
        public string Name { get; set; }

        public List<TaskItem> Tasks { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
