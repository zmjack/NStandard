using System;

namespace NStandard.Text.Json;

#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;

/// <summary>
/// Serialize interface by the value instance type.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public class JsonValueAttribute<T> : JsonConverterAttribute where T : IJsonValue
{
    public JsonValueAttribute() : base(typeof(JsonValueConverter<T>))
    {
    }
}
#else
[Obsolete("Must be manually configured based on the actual serializer used.")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public class JsonValueAttribute<T> : Attribute where T : IJsonValue
{
    public JsonValueAttribute()
    {
    }
}
#endif
