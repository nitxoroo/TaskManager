using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Application.ServicesContract
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
    }
}
