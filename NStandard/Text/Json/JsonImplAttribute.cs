using System;

namespace NStandard.Text.Json;

#if NET5_0_OR_GREATER
using System.Text.Json.Serialization;

/// <summary>
/// Serialize interface by instance type.
/// </summary>
/// <typeparam name="T"></typeparam>
[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
public class JsonImplAttribute<T> : JsonConverterAttribute
{
    public JsonImplAttribute() : base(typeof(JsonImplConverter<T>))
    {
    }
}

/// <summary>
/// Serialize instance by interface type.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TSerialize"></typeparam>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public class JsonImplAttribute<T, TSerialize> : JsonConverterAttribute
{
    public JsonImplAttribute() : base(typeof(JsonImplConverter<T, TSerialize>))
    {
    }
}
#else
/// <summary>
/// Serialize interface by instance type.
/// </summary>
/// <typeparam name="T"></typeparam>
[Obsolete("Must be manually configured based on the actual serializer used.")]
[AttributeUsage(AttributeTargets.Interface, AllowMultiple = false)]
public class JsonImplAttribute<T> : Attribute
{
    public JsonImplAttribute()
    {
    }
}

/// <summary>
/// Serialize instance by interface type.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TSerialize"></typeparam>
[Obsolete("Must be manually configured based on the actual serializer used.")]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, AllowMultiple = false)]
public class JsonImplAttribute<T, TSerialize> : Attribute
{
    public JsonImplAttribute()
    {
    }
}
#endif
