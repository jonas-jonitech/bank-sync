using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace bank_sync.GoCardless.utils;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string DateFormat = "yyyy-MM-dd";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException();
        }

        var dateString = reader.GetString();
        if (DateOnly.TryParseExact(dateString, DateFormat, out var date))
        {
            return date;
        }

        throw new JsonException($"Unable to parse '{dateString}' as a DateOnly.");
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat));
    }
}
