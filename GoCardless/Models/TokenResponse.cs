using System;
using System.Text.Json.Serialization;

namespace bank_sync.GoCardless.models;

public record TokenResponse
{
    [JsonPropertyName("access")]
    public required string AccessToken { get; set; }
}
