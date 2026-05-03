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
        public TaskService(ITaskRepo taskRepo)
        {
            _taskRepo = taskRepo;
        }

        public async Task<List<TaskItem>> GetAllTasks()
        {
            var res = await _taskRepo.GetAllTasks();

            if (res == null) throw new Exception("No tasks found");
            return res;
        }

        public async Task<TaskItem> GetTaskById(Guid id)
        {
            var res = await _taskRepo.GetTaskById(id);
            if (res == null) throw new Exception("No task found with the given id");
            return res;
        }

        public async Task<TaskItem> AddTask(AddTaskDto dto)
        {
            if(string.IsNullOrEmpty(dto.UserId)) throw new ArgumentException("UserId is required");

            
            var task = new TaskItem
            {
                Title = dto.Title,
                Description = dto.Description,
                IsCompleted = false,
                CreatedAt = DateTime.UtcNow,
                UserID=Guid.Parse(dto.UserId)
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
    }
}
