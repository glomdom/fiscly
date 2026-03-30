namespace Fiscly.Api.Dto;

public sealed record TransactionDto(string Merchant, string Category, decimal Amount, DateTime Date);