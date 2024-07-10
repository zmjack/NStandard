#if NET5_0
using System;
using System.Buffers;
using System.Text.Json;

namespace NStandard;

public static class JsonElementExtensions
{
    public static T? Deserialize<T>(this JsonElement @this, JsonSerializerOptions? options = null)
    {
        var bufferWriter = new ArrayBufferWriter<byte>();
        using (var writer = new Utf8JsonWriter(bufferWriter))
        {
            @this.WriteTo(writer);
        }
        return JsonSerializer.Deserialize<T>(bufferWriter.WrittenSpan, options)!;
    }

    public static object? Deserialize(this JsonElement @this, Type returnType, JsonSerializerOptions? options = null)
    {
        var bufferWriter = new ArrayBufferWriter<byte>();
        using (var writer = new Utf8JsonWriter(bufferWriter))
        {
            @this.WriteTo(writer);
        }
        return JsonSerializer.Deserialize(bufferWriter.WrittenSpan, returnType, options)!;
    }
}
#endif
