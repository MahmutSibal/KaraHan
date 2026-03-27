using KaraHan.Domain.Enums;

namespace KaraHan.Application.DTOs;

public record CreateTaskRequest(string Title, string? Description, TaskPriority Priority, DateTime DueDateUtc);

public record UpdateTaskRequest(string Title, string? Description, TaskPriority Priority, DateTime DueDateUtc, bool IsCompleted);

public record TaskDto(
    int Id,
    string Title,
    string? Description,
    TaskPriority Priority,
    DateTime DueDateUtc,
    bool IsCompleted,
    DateTime CreatedAtUtc,
    DateTime? CompletedAtUtc,
    bool IsOverdue);
