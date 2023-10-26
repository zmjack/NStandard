using Newtonsoft.Json;
using System;
using System.Net;

namespace NStandard.Json.Net.Converters;

public class IPAddressConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(IPAddress);

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        return reader.TokenType switch
        {
            JsonToken.Null => null,
            JsonToken.String => IPAddress.Parse(reader.Value as string),
            _ => throw new NotSupportedException($"{reader.TokenType} is not supported."),
        };
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        var valueType = value.GetType();
        if (!CanConvert(valueType)) throw new InvalidOperationException($"The converter specified on '{writer.Path}' is not compatible with the type '{typeof(IPAddress)}'.");
        serializer.Serialize(writer, ((IPAddress)value).ToString());
    }
}
