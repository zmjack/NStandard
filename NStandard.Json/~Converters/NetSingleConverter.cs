using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NStandard.Json
{
    public class NetSingleConverter : JsonConverter<float>
    {
        public override float Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var str = reader.GetString();
                return str switch
                {
                    "Infinity" => float.PositiveInfinity,
                    "-Infinity" => float.NegativeInfinity,
                    _ => float.NaN,
                };
            }
            return reader.GetSingle();
        }

        public override void Write(Utf8JsonWriter writer, float value, JsonSerializerOptions options)
        {
            if (float.IsNaN(value)) writer.WriteStringValue("NaN");
            else if (float.IsPositiveInfinity(value)) writer.WriteStringValue("Infinity");
            else if (float.IsNegativeInfinity(value)) writer.WriteStringValue("-Infinity");
            else writer.WriteNumberValue(value);
        }

    }
}
