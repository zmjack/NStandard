namespace NStandard.Text.Json;

#if NET5_0_OR_GREATER
using NStandard.Text.Json.Converters;
using System.Text.Json.Serialization;

/// <summary>
/// Serialize instance by interface type.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TSerialize"></typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public class JsonAsAttribute<T, TSerialize> : JsonConverterAttribute where T : TSerialize, new()
{
    public JsonAsAttribute() : base(typeof(JsonAsConverter<T, TSerialize>))
    {
    }
}
#else
/// <summary>
/// Serialize instance by interface type.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TSerialize"></typeparam>
[Obsolete("Must be manually configured based on the actual serializer used.")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public class JsonAsAttribute<T, TSerialize> : Attribute where T : TSerialize, new()
{
    public JsonAsAttribute()
    {
    }
}
#endif
