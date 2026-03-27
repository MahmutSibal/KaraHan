using KaraHan.Application.Abstractions;
using KaraHan.Domain.Entities;
using KaraHan.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KaraHan.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TaskRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(TaskItem task, CancellationToken cancellationToken = default)
        => await _dbContext.Tasks.AddAsync(task, cancellationToken);

    public async Task<TaskItem?> GetByIdForUserAsync(int taskId, int userId, CancellationToken cancellationToken = default)
        => await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId && x.UserId == userId, cancellationToken);

    public async Task<IReadOnlyList<TaskItem>> GetForUserAsync(int userId, CancellationToken cancellationToken = default)
        => await _dbContext.Tasks.Where(x => x.UserId == userId).ToListAsync(cancellationToken);

    public void Remove(TaskItem task)
        => _dbContext.Tasks.Remove(task);

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);
}
