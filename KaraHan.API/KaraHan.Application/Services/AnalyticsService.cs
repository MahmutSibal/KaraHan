using KaraHan.Application.Abstractions;
using KaraHan.Application.DTOs;

namespace KaraHan.Application.Services;

public class AnalyticsService : IAnalyticsService
{
    private static readonly (string Label, int StartInclusive, int EndExclusive)[] Windows =
    [
        ("06:00-12:00", 6, 12),
        ("12:00-18:00", 12, 18),
        ("18:00-24:00", 18, 24),
        ("00:00-06:00", 0, 6)
    ];

    private readonly ITaskRepository _taskRepository;

    public AnalyticsService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<DashboardSummaryDto> GetDashboardAsync(int userId, CancellationToken cancellationToken = default)
    {
        var tasks = await _taskRepository.GetForUserAsync(userId, cancellationToken);

        var totalTasks = tasks.Count;
        var completedTasks = tasks.Count(t => t.IsCompleted);
        var overdueTasks = tasks.Count(t => !t.IsCompleted && t.DueDateUtc < DateTime.UtcNow);
        var delayedRate = totalTasks == 0 ? 0 : Math.Round((decimal)overdueTasks / totalTasks, 2);

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var dailyPerformance = Enumerable.Range(0, 7)
            .Select(offset => today.AddDays(-offset))
            .OrderBy(date => date)
            .Select(date =>
            {
                var completedCount = tasks.Count(t => t.CompletedAtUtc.HasValue && DateOnly.FromDateTime(t.CompletedAtUtc.Value) == date);
                return new DailyPerformanceDto(date, completedCount);
            })
            .ToList();

        var completedByHour = tasks
            .Where(t => t.CompletedAtUtc.HasValue)
            .GroupBy(t => t.CompletedAtUtc!.Value.Hour)
            .ToDictionary(group => group.Key, group => group.Count());

        var productiveWindows = Windows
            .Select(window => new TimeWindowPerformanceDto(
                window.Label,
                completedByHour
                    .Where(x => x.Key >= window.StartInclusive && x.Key < window.EndExclusive)
                    .Sum(x => x.Value)))
            .OrderByDescending(x => x.CompletedCount)
            .ToList();

        var suggestions = BuildSuggestions(overdueTasks, totalTasks, productiveWindows);

        return new DashboardSummaryDto(
            totalTasks,
            completedTasks,
            overdueTasks,
            delayedRate,
            dailyPerformance,
            productiveWindows,
            suggestions);
    }

    private static IReadOnlyList<string> BuildSuggestions(int overdueTasks, int totalTasks, IReadOnlyList<TimeWindowPerformanceDto> productiveWindows)
    {
        var suggestions = new List<string>();

        if (totalTasks > 0 && (decimal)overdueTasks / totalTasks > 0.3m)
        {
            suggestions.Add("Geciken gorev orani yuksek. Daha kucuk alt gorevler olusturmayi deneyin.");
        }

        var topWindow = productiveWindows.FirstOrDefault();
        if (topWindow is not null && topWindow.CompletedCount > 0)
        {
            suggestions.Add($"En verimli zaman araliginiz {topWindow.WindowLabel}. Oncelikli gorevleri bu araliga planlayin.");
        }
        else
        {
            suggestions.Add("Daha anlamli analiz icin gorev tamamlama kaydi biriktirin.");
        }

        if (suggestions.Count == 0)
        {
            suggestions.Add("Planinizi haftalik olarak gozden gecirin ve teslim tarihlerini gercekci belirleyin.");
        }

        return suggestions;
    }
}
