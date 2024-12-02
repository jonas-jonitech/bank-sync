namespace bank_sync.Core;

public record Transaction
{
    public required decimal Amount { get; set; }
    public required TransactionType Type { get; set; }
    public required string Payee { get; set; }
    public DateOnly Date { get; set; }
}

public enum TransactionType 
{
    Income,
    Expense
}
