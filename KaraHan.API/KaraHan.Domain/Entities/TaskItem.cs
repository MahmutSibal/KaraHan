using KaraHan.Domain.Enums;

namespace KaraHan.Domain.Entities;

public class TaskItem
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    public DateTime DueDateUtc { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAtUtc { get; set; }

    public AppUser User { get; set; } = null!;
}
