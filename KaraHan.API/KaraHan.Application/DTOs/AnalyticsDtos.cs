namespace KaraHan.Application.DTOs;

public record DashboardSummaryDto(
    int TotalTasks,
    int CompletedTasks,
    int OverdueTasks,
    decimal DelayedTaskRate,
    IReadOnlyList<DailyPerformanceDto> DailyPerformance,
    IReadOnlyList<TimeWindowPerformanceDto> ProductiveWindows,
    IReadOnlyList<string> Suggestions);

public record DailyPerformanceDto(DateOnly Date, int CompletedCount);

public record TimeWindowPerformanceDto(string WindowLabel, int CompletedCount);
