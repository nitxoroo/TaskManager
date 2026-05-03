using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Features.Tasks.Command;
using TaskManager.Application.Features.Tasks.Command.Handlers;
using TaskManager.Application.Features.Tasks.Queries;

namespace TaskManager.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagerController : ControllerBase
    {
        private IMediator _mediator;
        public TaskManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllTasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = await _mediator.Send(new GetAllTaskQuerry());
            return Ok(result);
        }

        [HttpGet("GetMyTasks")]
        public async Task<IActionResult> GetAllUserTask()
        {
            var result = await _mediator.Send(new GetAllUserTaskQuery());
            return Ok(result);
        }

        [HttpGet("GetTaskById/{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var result = await _mediator.Send(new GetTaskByIdQuerry { Id = id });
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost("CreateTask")]
        public async Task<IActionResult> AddTask(AddTaskCommand addTaskCommand)
        {
            var result = await _mediator.Send(addTaskCommand);
            return Ok(result);
        }

        [HttpPut("UpdateTask")]
        public async Task<IActionResult> UpdateTask(UpdateTaskCommand updateTaskCommand)
        {
            var result = await _mediator.Send(updateTaskCommand);
            return Ok(result);
        }

        [HttpDelete("DeleteTask")]
        public async Task<IActionResult> DeleteTask(DeleteTaskCommand deleteTaskCommand )
        {
            var result = await _mediator.Send(deleteTaskCommand);
            return Ok(result);
        }
    }
}
