using KaraHan.Application.Abstractions;
using KaraHan.Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KaraHan.API.Controllers;

[Authorize]
[Route("api/dashboard")]
public class DashboardController : BaseApiController
{
    private readonly IAnalyticsService _analyticsService;

    public DashboardController(IAnalyticsService analyticsService)
    {
        _analyticsService = analyticsService;
    }

    [HttpGet("summary")]
    public async Task<ActionResult<DashboardSummaryDto>> GetSummary(CancellationToken cancellationToken)
    {
        var summary = await _analyticsService.GetDashboardAsync(UserId, cancellationToken);
        return Ok(summary);
    }
}
