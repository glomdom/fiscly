using System.Security.Claims;
using Fiscly.Api.Data;
using Fiscly.Api.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiscly.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase {
    private readonly AppDbContext _db;

    public TransactionsController(AppDbContext db) {
        _db = db;
    }

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<FinancialMetricsResponseDto>> GetAll() {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null) {
            return BadRequest(new ProblemDetails {
                Detail = "User was not found.",
                Status = StatusCodes.Status400BadRequest,
            });
        }

        var transactions = await _db.Transactions
            .AsNoTracking()
            .Where(x => x.UserId == user.Id)
            .OrderByDescending(x => x.Date)
            .Select(x => new TransactionDto(x.Merchant, x.Category, x.Amount, x.Date))
            .ToListAsync();

        return Ok(
            new TransactionsResponseDto(transactions)
        );
    }

    [HttpGet("breakdown")]
    [Authorize]
    public async Task<ActionResult<FinancialBreakdownResponseDto>> Breakdown() {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        if (!await _db.Users.AnyAsync(x => x.Id == userId)) {
            return BadRequest(new ProblemDetails {
                Detail = "User was not found.",
                Status = StatusCodes.Status400BadRequest,
            });
        }

        var flatData = await _db.Transactions
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .GroupBy(x => new { x.Date.Year, x.Date.Month, x.Category })
            .Select(g => new {
                g.Key.Year,
                g.Key.Month,
                g.Key.Category,
                Total = g.Sum(t => t.Amount)
            })
            .ToListAsync();

        var monthlyBreakdowns = flatData
            .GroupBy(x => new { x.Year, x.Month })
            .Select(monthGroup => new MonthlyBreakdownDto(
                monthGroup.Key.Year,
                monthGroup.Key.Month,
                monthGroup.Select(c => new CategorySummaryDto(c.Category, c.Total)).ToList(),
                monthGroup.Sum(c => c.Total)
            ))
            .OrderByDescending(x => x.Year)
            .ThenByDescending(x => x.Month)
            .ToList();

        return Ok(new FinancialBreakdownResponseDto(monthlyBreakdowns));
    }
}