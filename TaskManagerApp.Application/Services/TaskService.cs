using Microsoft.Extensions.Logging;
using TaskManagerApp.Application.Interfaces;
using TaskManagerApp.Domain.Entities;
using TaskManagerApp.Domain.Interfaces;
using TaskManagerApp.Domain.ValueObjects;

namespace TaskManagerApp.Application.Services;

public class TaskService : ITaskService
{
    private readonly IRepository<TaskEntity> _taskRepository;
    private readonly ILogger<TaskService> _logger;

    public TaskService(IRepository<TaskEntity> taskRepository, ILogger<TaskService> logger)
    {
        _taskRepository = taskRepository;
        _logger = logger;
    }

    public async Task<TaskEntity> AddTaskAsync(string description, DateTime startDate, DateTime endDate, Location location, long userId)
    {
        var task = new TaskEntity(description, startDate, endDate)
        {
            Location = location,
            AssignedToUserId = userId
        };

        await _taskRepository.AddAsync(task);
        return task;
    }

    public async Task<IEnumerable<TaskEntity>> GetTasksForUserAsync(long userId)
    {
        return await _taskRepository.GetAllAsync(t => t.AssignedToUserId == userId);
    }

    async Task ITaskService.AddTaskAsync(TaskEntity task)
    {
        if (task == null)
            throw new ArgumentException("A tarefa não pode ser nula");

        if (task.StartDate < task.EndDate)
            throw new ArgumentException("A data fim não pode ser menor que a data de início.");

        await _taskRepository.AddAsync(task);
    }

    async Task ITaskService.DeleteTaskAsync(long id)
    {
        await _taskRepository.DeleteAsync(id);
    }

    async Task<TaskEntity> ITaskService.GetTaskByIdAsync(long id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
            throw new KeyNotFoundException($"Não encontrada Task com id {id}.");
        return task;
    }

    async Task ITaskService.UpdateTaskAsync(TaskEntity task)
    {
        if (task == null)
            throw new ArgumentNullException(nameof(task), "Task não pode ser nula.");
        var existingTask = await _taskRepository.GetByIdAsync(task.Id);
        if (existingTask == null)
            throw new KeyNotFoundException($"Não encontrada Task com id {task.Id}.");

        if (task.StartDate < task.EndDate)
            throw new ArgumentException("A data fim não pode ser menor que a data de início.");

        await _taskRepository.UpdateAsync(task);
        _logger.LogInformation("A tarefa {TaskId} foi atualizada com sucesso.", task.Guid);
    }
}
