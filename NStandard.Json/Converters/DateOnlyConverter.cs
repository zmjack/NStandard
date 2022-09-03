using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace NStandard.Json.Converters
{
#if NET6_0_OR_GREATER
    public class DateOnlyConverter : JsonConverter<DateOnly>
    {
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var str = reader.GetString();
            var dt = DateTime.Parse(str).ToLocalTime();
            return DateOnly.FromDateTime(dt);
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            var isoDate = value.ToString("O");
            writer.WriteStringValue(isoDate);
        }
    }
#endif
}
