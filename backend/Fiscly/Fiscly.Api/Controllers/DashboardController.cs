using Fiscly.Api.Data;
using Fiscly.Api.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<FinancialReportDto>> Aggregate() {
        return Ok(new FinancialReportDto(new FinancialMetricsDto(67, 67, 67, 67, 67)));
    }
}