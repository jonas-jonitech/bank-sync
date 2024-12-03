using System;
using System.Text.Json.Serialization;

namespace bank_sync.Notion.models;

public class PageRequest
{
    [JsonPropertyName("parent")]
    public required Parent parent { get; set; }

    [JsonPropertyName("properties")]
    public required PageProperties Properties { get; set; }
}

public class Parent 
{
    [JsonPropertyName("database_id")]
    public required string DatabaseId { get; set; }
}

public class PageProperties
{
    [JsonPropertyName("ID")]
    public required TitleProperty Id { get; set; }

    [JsonPropertyName("Date")]
    public required DateProperty Date { get; set; }

    [JsonPropertyName("Payee")]
    public required RichTextProperty Payee { get; set; }

    [JsonPropertyName("Inflow")]
    public NumberProperty? Inflow { get; set; }

    [JsonPropertyName("Outflow")]
    public NumberProperty? Outflow { get; set; }
}
