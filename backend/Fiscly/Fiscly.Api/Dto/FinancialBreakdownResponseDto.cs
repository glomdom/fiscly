namespace Fiscly.Api.Dto;

public sealed record FinancialBreakdownResponseDto(List<MonthlyBreakdownDto> MonthlyBreakdowns);

public sealed record MonthlyBreakdownDto(
    int Year,
    int Month,
    List<CategorySummaryDto> Categories,
    decimal MonthlyTotal
);

public record CategorySummaryDto(string Category, decimal TotalAmount);