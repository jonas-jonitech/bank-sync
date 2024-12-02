using System;
using System.Text.Json.Serialization;
using bank_sync.GoCardless.utils;

namespace bank_sync.GoCardless.models;

public record TransactionResponse
{
    [JsonPropertyName("transactions")]
    public TransactionList Transactions { get; set; }
}

public record TransactionList
{
    [JsonPropertyName("booked")]
    public List<Transaction> Booked { get; set; }

    [JsonPropertyName("pending")]
    public List<Transaction> Pending { get; set; }
}

public record Transaction
{
    [JsonPropertyName("transactionId")]
    public string TransactionId { get; set; }

    [JsonPropertyName("transactionAmount")]
    public TransactionAmount TransactionAmount { get; set; }

    [JsonPropertyName("creditorName")]
    public string CreditorName { get; set; }

    [JsonPropertyName("debitorName")]
    public string DebitorName { get; set; }

    [JsonPropertyName("valueDate")]
    [JsonConverter(typeof(DateOnlyJsonConverter))]
    public DateOnly ValueDate { get; set; }
}

public record TransactionAmount 
{
    [JsonPropertyName("amount")]
    public decimal Amount { get; set; }
}


