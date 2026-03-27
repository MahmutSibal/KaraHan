using KaraHan.Application.DTOs;

namespace KaraHan.Application.Abstractions;

public interface ITaskService
{
    Task<IReadOnlyList<TaskDto>> GetTasksAsync(int userId, CancellationToken cancellationToken = default);
    Task<TaskDto> CreateTaskAsync(int userId, CreateTaskRequest request, CancellationToken cancellationToken = default);
    Task<TaskDto?> UpdateTaskAsync(int userId, int taskId, UpdateTaskRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteTaskAsync(int userId, int taskId, CancellationToken cancellationToken = default);
}
