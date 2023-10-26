using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NStandard.Json.Converters;

public class IPAddressConverter : JsonConverter<IPAddress>
{
    public override bool CanConvert(Type objectType) => objectType == typeof(IPAddress);

    public override IPAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();
        return IPAddress.Parse(str);
    }

    public override void Write(Utf8JsonWriter writer, IPAddress value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString());
    }
}
