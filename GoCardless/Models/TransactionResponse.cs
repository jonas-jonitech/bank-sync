using System;
using System.Text.Json.Serialization;
using bank_sync.GoCardless.utils;

namespace bank_sync.GoCardless.models;

public record TransactionResponse
{
    [JsonPropertyName("transactions")]
    public TransactionList Transactions { get; set; } = default!;
}

public record TransactionList
{
    [JsonPropertyName("booked")]
    public List<Transaction> Booked { get; set; } = [];

    [JsonPropertyName("pending")]
    public List<Transaction> Pending { get; set; } = [];
}

public record Transaction
{
    [JsonPropertyName("transactionId")]
    public string TransactionId { get; set; } = string.Empty!;

    [JsonPropertyName("transactionAmount")]
    public TransactionAmount TransactionAmount { get; set; } = default!;

    [JsonPropertyName("creditorName")]
    public string CreditorName { get; set; } = string.Empty;

    [JsonPropertyName("debitorName")]
    public string DebitorName { get; set; } = string.Empty;

    [JsonPropertyName("valueDate")]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly ValueDate { get; set; } = default!;
}

public record TransactionAmount 
{
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; } = default;
}


