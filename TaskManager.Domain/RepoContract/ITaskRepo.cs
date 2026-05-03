using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.RepoContract
{
    public interface ITaskRepo
    {
        Task<List<TaskItem>> GetAllTasks();
        Task<List<TaskItem>> GetTaskByUserId(Guid id);
        Task<TaskItem> GetTaskById(Guid id);
        Task<TaskItem> AddTask(TaskItem task);

        Task<TaskItem> UpdateTask(TaskItem task);

        Task<TaskItem> DeleteTask(Guid id);
    }

}
