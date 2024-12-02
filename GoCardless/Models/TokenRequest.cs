using System.Text.Json.Serialization;

namespace bank_sync.GoCardless.Models;

public record TokenRequest
{
    [JsonPropertyName("secret_id")]
    public required string SecretId { get; set; }

    [JsonPropertyName("secret_key")]
    public required string SecretKey { get; set; }
}