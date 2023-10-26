using System;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace NStandard.Json.Converters;

public class PhysicalAddressConverter : JsonConverter<PhysicalAddress>
{
    public override bool CanConvert(Type objectType) => objectType == typeof(PhysicalAddress);

    public override PhysicalAddress Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();
        return PhysicalAddress.Parse(str);
    }

    public override void Write(Utf8JsonWriter writer, PhysicalAddress value, JsonSerializerOptions options)
    {
        var bytes = ((PhysicalAddress)value).GetAddressBytes();
        var enumerator = bytes.GetEnumerator();
        var sb = new StringBuilder(30);
        if (enumerator.MoveNext())
        {
            sb.Append(((byte)enumerator.Current).ToString("X2"));
            while (enumerator.MoveNext())
            {
                sb.Append('-');
                sb.Append(((byte)enumerator.Current).ToString("X2"));
            }
        }

        writer.WriteStringValue(sb.ToString());
    }
}
