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
    public Date Date { get; set; } = default!;
}

public class Date
{
    [JsonPropertyName("start")]
    public string Start { get; set; } = default!;
}

public class RichTextProperty
{
    [JsonPropertyName("rich_text")]
    public List<RichText> RichText { get; set; } = default!;
}

public class RichText
{
    [JsonPropertyName("text")]
    public Text Text { get; set; } = default!;
}

public class Text
{
    [JsonPropertyName("content")]
    public string Content { get; set; } = default!;
}

public class StatusProperty
{
    [JsonPropertyName("status")]
    public Status Status { get; set; } = default!;
}

public class Status
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = default!;
}

public class Select
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("color")]
    public string Color { get; set; } = string.Empty;
}

public class TitleProperty
{
    [JsonPropertyName("title")]
    public List<RichText> Title { get; set; } = [];
}
