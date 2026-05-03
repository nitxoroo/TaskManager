using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Application.DTOs;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.ServicesContract
{
    public interface ITaskService
    {
        Task<List<TaskItem>> GetAllTasks();
        Task<TaskItem> GetTaskById(Guid id);
        Task<TaskItem> AddTask(AddTaskDto dto);

        Task<TaskItem> UpdateTask(Guid id, UpdateTaskDto dto);
        Task<TaskItem> DeleteTask(Guid id);

    }
}
