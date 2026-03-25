namespace Fiscly.Api.Domain;

public sealed class Transaction {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public string Merchant { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}