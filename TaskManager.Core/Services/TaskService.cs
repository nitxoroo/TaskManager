using Microsoft.JSInterop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using TaskManager.Application.DTOs;
using TaskManager.Application.RepoContract;
using TaskManager.Application.ServicesContract;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Identities;

namespace TaskManager.Infrastructure.Services
{
    public class TaskService: ITaskService
    {
        private readonly ITaskRepo _taskRepo;
        private readonly ICurrentUserService _currentUserService;
        public TaskService(ITaskRepo taskRepo,ICurrentUserService currentUserService)
        {
            _taskRepo = taskRepo;
            _currentUserService = currentUserService;
        }

        public async Task<List<TaskItem>> GetAllTasks()
        {
            var res = await _taskRepo.GetAllTasks();

            if (res == null) throw new Exception("No tasks found");
            return res;
        }

        public async Task<TaskItem> GetTaskById(Guid id)
        {
            return await _taskRepo.GetTaskById(id);
        }

        public async Task<List<TaskItem>> GetAllUserTask()
        {
            var userId = GetCurrentUserId();
            var res = await _taskRepo.GetTaskByUserId(userId);
            if (res == null) throw new Exception("No task found ");
            return res;
        }

        public async Task<TaskItem> AddTask(AddTaskDto dto)
        {
            

            
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                UserID= GetCurrentUserId(),
            };
            var res = await _taskRepo.AddTask(task);
            return res;
        }

        public async Task<TaskItem> UpdateTask(Guid id, UpdateTaskDto dto)
        {
            var task = await _taskRepo.GetTaskById(id);
            if (task == null) throw new Exception("No task found with the given id");
            task.Title = dto.Title ?? task.Title;
            task.Description = dto.Description ?? task.Description;
            task.IsCompleted = dto.IsCompleted ?? task.IsCompleted;
            var res = await _taskRepo.UpdateTask(task);
            return res;
        }


        public async Task<TaskItem> DeleteTask(Guid id)
        {
            var res = await _taskRepo.DeleteTask(id);
            if (res == null) throw new Exception("No task found with the given id");
            return res;
        }

        private Guid GetCurrentUserId()
        {
            if (_currentUserService.UserId == null)
                throw new UnauthorizedAccessException("User not authenticated");

            return _currentUserService.UserId.Value;
        }
    }
}
