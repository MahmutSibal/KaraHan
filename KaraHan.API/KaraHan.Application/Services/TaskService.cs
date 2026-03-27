using KaraHan.Application.Abstractions;
using KaraHan.Application.DTOs;
using KaraHan.Domain.Entities;

namespace KaraHan.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IReadOnlyList<TaskDto>> GetTasksAsync(int userId, CancellationToken cancellationToken = default)
    {
        var tasks = await _taskRepository.GetForUserAsync(userId, cancellationToken);
        return tasks
            .OrderBy(t => t.IsCompleted)
            .ThenBy(t => t.DueDateUtc)
            .Select(Map)
            .ToList();
    }

    public async Task<TaskDto> CreateTaskAsync(int userId, CreateTaskRequest request, CancellationToken cancellationToken = default)
    {
        var task = new TaskItem
        {
            UserId = userId,
            Title = request.Title.Trim(),
            Description = request.Description?.Trim(),
            Priority = request.Priority,
            DueDateUtc = DateTime.SpecifyKind(request.DueDateUtc, DateTimeKind.Utc),
            IsCompleted = false
        };

        await _taskRepository.AddAsync(task, cancellationToken);
        await _taskRepository.SaveChangesAsync(cancellationToken);

        return Map(task);
    }

    public async Task<TaskDto?> UpdateTaskAsync(int userId, int taskId, UpdateTaskRequest request, CancellationToken cancellationToken = default)
    {
        var task = await _taskRepository.GetByIdForUserAsync(taskId, userId, cancellationToken);
        if (task is null)
        {
            return null;
        }

        task.Title = request.Title.Trim();
        task.Description = request.Description?.Trim();
        task.Priority = request.Priority;
        task.DueDateUtc = DateTime.SpecifyKind(request.DueDateUtc, DateTimeKind.Utc);

        if (!task.IsCompleted && request.IsCompleted)
        {
            task.IsCompleted = true;
            task.CompletedAtUtc = DateTime.UtcNow;
        }
        else if (task.IsCompleted && !request.IsCompleted)
        {
            task.IsCompleted = false;
            task.CompletedAtUtc = null;
        }

        await _taskRepository.SaveChangesAsync(cancellationToken);
        return Map(task);
    }

    public async Task<bool> DeleteTaskAsync(int userId, int taskId, CancellationToken cancellationToken = default)
    {
        var task = await _taskRepository.GetByIdForUserAsync(taskId, userId, cancellationToken);
        if (task is null)
        {
            return false;
        }

        _taskRepository.Remove(task);
        await _taskRepository.SaveChangesAsync(cancellationToken);
        return true;
    }

    private static TaskDto Map(TaskItem task)
    {
        var isOverdue = !task.IsCompleted && task.DueDateUtc < DateTime.UtcNow;
        return new TaskDto(
            task.Id,
            task.Title,
            task.Description,
            task.Priority,
            task.DueDateUtc,
            task.IsCompleted,
            task.CreatedAtUtc,
            task.CompletedAtUtc,
            isOverdue);
    }
}
