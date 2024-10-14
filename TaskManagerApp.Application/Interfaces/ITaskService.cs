using TaskManagerApp.Domain.Entities;

namespace TaskManagerApp.Application.Interfaces;

public interface ITaskService
{
    public Task AddTaskAsync(TaskEntity task);
    public Task<IEnumerable<TaskEntity>> GetTasksForUserAsync(long userId);
    public Task<TaskEntity> GetTaskByIdAsync(long id);
    public Task UpdateTaskAsync(TaskEntity task);
    public Task DeleteTaskAsync(long id);
}
