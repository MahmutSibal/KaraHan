using KaraHan.Application.DTOs;

namespace KaraHan.Application.Abstractions;

public interface IAnalyticsService
{
    Task<DashboardSummaryDto> GetDashboardAsync(int userId, CancellationToken cancellationToken = default);
}
