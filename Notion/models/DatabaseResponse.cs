using System.Text.Json.Serialization;

namespace bank_sync.Notion.models;

public class PageList
{
    [JsonPropertyName("results")]
    public List<Page> Results { get; set; } = default!;

    [JsonPropertyName("next_cursor")]
    public string NextCursor { get; set; } = default!;

    [JsonPropertyName("has_more")]
    public bool HasMore { get; set; }
}

public class Page
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;

    [JsonPropertyName("properties")]
    public Properties Properties { get; set; } = default!;
}


public class Properties
{
    [JsonPropertyName("Outflow")]
    public NumberProperty Outflow { get; set; } = default!;

    [JsonPropertyName("Date")]
    public DateProperty Date { get; set; } = default!;

    [JsonPropertyName("Payee")]
    public RichTextProperty Payee { get; set; } = default!;

    [JsonPropertyName("Inflow")]
    public NumberProperty Inflow { get; set; } = default!;

    [JsonPropertyName("Status")]
    public StatusProperty Status { get; set; } = default!;

    [JsonPropertyName("ID")]
    public TitleProperty Id { get; set; } = default!;
}

public class NumberProperty
{
    [JsonPropertyName("number")]
    public double? Number { get; set; }
}

public class DateProperty
{
    [JsonPropertyName("date")]
    public Date? Date { get; set; }
}

public class Date
{
    [JsonPropertyName("start")]
    public string? Start { get; set; }
}

public class RichTextProperty
{
    [JsonPropertyName("rich_text")]
    public List<RichText>? RichText { get; set; }
}

public class RichText
{
    [JsonPropertyName("text")]
    public Text? Text { get; set; }
}

public class Text
{
    [JsonPropertyName("content")]
    public string? Content { get; set; }
}

public class StatusProperty
{
    [JsonPropertyName("status")]
    public Status? Status { get; set; }
}

public class Status
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
}

public class Select
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("color")]
    public string? Color { get; set; }
}

public class TitleProperty
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("title")]
    public List<RichText>? Title { get; set; }
}
