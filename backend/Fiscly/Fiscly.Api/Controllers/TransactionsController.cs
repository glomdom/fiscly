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
    public async Task<ActionResult<FinancialReportDto>> GetAll() {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == userId);
        if (user is null) {
            return BadRequest(new ProblemDetails {
                Detail = "User was not found.",
                Status = StatusCodes.Status400BadRequest,
            });
        }

        var transactions = _db.Transactions
            .AsNoTracking()
            .Where(x => x.UserId == user.Id)
            .Select(x => new TransactionDto(x.Merchant, x.Category, x.Amount, x.Date))
            .ToList();

        return Ok(
            new TransactionsResponseDto(transactions)
        );
    }
}