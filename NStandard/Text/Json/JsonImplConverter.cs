#if NET5_0_OR_GREATER
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NStandard.Text.Json;

public class JsonImplConverter<T> : JsonConverter<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotSupportedException();
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            JsonSerializer.Serialize<string?>(writer, null);
        }
        else
        {
            var type = value.GetType();
            JsonSerializer.Serialize(writer, value, type, options);
        }
    }
}

public class JsonImplConverter<T, TSerialize> : JsonConverter<T>
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        if (value is null)
        {
            JsonSerializer.Serialize<string?>(writer, null);
        }
        else
        {
            var type = typeof(TSerialize);
            JsonSerializer.Serialize(writer, value, type, options);
        }
    }
}
#endif
