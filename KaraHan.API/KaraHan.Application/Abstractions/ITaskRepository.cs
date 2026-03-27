using KaraHan.Domain.Entities;

namespace KaraHan.Application.Abstractions;

public interface ITaskRepository
{
    Task AddAsync(TaskItem task, CancellationToken cancellationToken = default);
    Task<TaskItem?> GetByIdForUserAsync(int taskId, int userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TaskItem>> GetForUserAsync(int userId, CancellationToken cancellationToken = default);
    void Remove(TaskItem task);
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}
