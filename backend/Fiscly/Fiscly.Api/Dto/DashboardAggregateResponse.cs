namespace Fiscly.Api.Dto;

public sealed record FinancialReportDto(
    FinancialMetricsDto Metrics
);

public sealed record FinancialMetricsDto(
    decimal TotalLiquidity,
    decimal MonthlyIncome,
    decimal MonthlyExpenses,
    decimal TotalSavings,
    decimal Investments
);