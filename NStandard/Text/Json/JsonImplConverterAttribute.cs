using System;
#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;

namespace NStandard.Text.Json;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
public class JsonImplConverterAttribute<T> : JsonConverterAttribute
{
    public JsonImplConverterAttribute() : base(typeof(JsonImplConverter<T>))
    {
    }
}
#else
/// <summary>
/// Must be manually configured based on the actual serializer used.
/// </summary>
/// <typeparam name="T"></typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
public class JsonImplConverterAttribute<T> : Attribute
{
    public JsonImplConverterAttribute()
    {
    }
}
#endif
