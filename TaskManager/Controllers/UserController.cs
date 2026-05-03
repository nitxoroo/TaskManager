using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.Users.Queries;

namespace TaskManager.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")] // 🔥 IMPORTANT
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _mediator.Send(new GetAllUserQuery());
            return Ok(users);
        }
    }
}
