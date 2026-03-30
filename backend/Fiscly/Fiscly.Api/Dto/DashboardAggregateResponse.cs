namespace Fiscly.Api.Dto;

public sealed record FinancialMetricsResponseDto(
    FinancialMetricsDto Metrics
);

public sealed record FinancialMetricsDto(
    decimal TotalLiquidity,
    decimal MonthlyIncome,
    decimal MonthlyExpenses,
    decimal TotalSavings,
    decimal Investments
);