using System.Security.Claims;
using Fiscly.Api.Data;
using Fiscly.Api.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiscly.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase {
    private readonly AppDbContext _db;

    public DashboardController(AppDbContext db) {
        _db = db;
    }

    [HttpGet("aggregate")]
    [Authorize]
    public async Task<ActionResult<FinancialMetricsResponseDto>> Aggregate() {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null) {
            return BadRequest(new ProblemDetails {
                Detail = "User was not found.",
                Status = StatusCodes.Status400BadRequest,
            });
        }

        // perf hacks
        var now = DateTime.UtcNow;
        var startOfMonth = new DateTime(now.Year, now.Month, 1).ToUniversalTime();
        var startOfNextMonth = startOfMonth.AddMonths(1);

        var stats = await _db.Transactions
            .AsNoTracking()
            .Where(x => x.UserId == user.Id)
            .GroupBy(x => 1)
            .Select(g => new {
                TotalLiquidity = g.Sum(x => x.Amount),
                Income = g.Where(x => x.Date >= startOfMonth && x.Date < startOfNextMonth && x.Amount > 0)
                    .Sum(x => x.Amount),
                Expenses = g.Where(x => x.Date >= startOfMonth && x.Date < startOfNextMonth && x.Amount < 0)
                    .Sum(x => x.Amount),
                TotalSaving = g.Where(x => x.Category == "Saving").Sum(x => x.Amount),
                TotalInvestments = g.Where(x => x.Category == "Investment").Sum(x => x.Amount),
            })
            .FirstOrDefaultAsync();

        var totalLiquidity = stats?.TotalLiquidity ?? 0;
        var monthlyIncome = stats?.Income ?? 0;
        var monthlyExpenses = Math.Abs(stats?.Expenses ?? 0);
        var totalSavings = stats?.TotalSaving ?? 0;
        var totalInvestments = Math.Abs(stats?.TotalInvestments ?? 0);

        return Ok(new FinancialMetricsResponseDto(new FinancialMetricsDto(totalLiquidity, monthlyIncome, monthlyExpenses, totalSavings, totalInvestments)));
    }
}