#if NET5_0_OR_GREATER
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NStandard.Text.Json;

public class JsonValueConverter<T> : JsonConverter<T> where T : IJsonValue
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (value is null || value.Value is null)
        {
            JsonSerializer.Serialize(writer, null, options);
        }
        else
        {
            var type = value.Value.GetType();
            JsonSerializer.Serialize(writer, value.Value, type, options);
        }
    }
}
#endif
