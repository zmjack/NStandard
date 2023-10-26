using Newtonsoft.Json;
using System;

namespace NStandard.Json.Net.Converters;

#if NET6_0_OR_GREATER
public class DateOnlyConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(DateOnly) || objectType == typeof(DateOnly?);

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return reader.TokenType switch
        {
            JsonToken.Null => null,
            JsonToken.String => DateOnly.FromDateTime(DateTime.Parse(reader.Value as string).ToLocalTime()),
            JsonToken.Date => DateOnly.FromDateTime((DateTime)reader.Value),
            _ => throw new NotSupportedException($"{reader.TokenType} is not supported."),
        };
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var valueType = value.GetType();
        if (!CanConvert(valueType)) throw new InvalidOperationException($"The converter specified on '{writer.Path}' is not compatible with the type '{typeof(DateOnly)}'.");
        serializer.Serialize(writer, ((DateOnly)value).ToString("O"));
    }
}
#endif
