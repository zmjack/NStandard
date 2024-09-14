namespace NStandard.Text.Json;

#if NET5_0_OR_GREATER
using NStandard.Text.Json.Converters;
using System.Text.Json.Serialization;

/// <summary>
/// Serialize interface by instance type.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TDeserialize"></typeparam>
[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
public class JsonImplAttribute<T, TDeserialize> : JsonConverterAttribute where TDeserialize : T, new()
{
    public JsonImplAttribute() : base(typeof(JsonImplConverter<T, TDeserialize>))
    {
    }
}
#else
/// <summary>
/// Serialize interface by instance type.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TDeserialize"></typeparam>
[Obsolete("Must be manually configured based on the actual serializer used.")]
[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
public class JsonImplAttribute<T, TDeserialize> : Attribute where TDeserialize : T, new()
{
    public JsonImplAttribute()
    {
    }
}
#endif
