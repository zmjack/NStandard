﻿using NStandard.Reflection;
using System.ComponentModel;
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET451_OR_GREATER
using System.Dynamic;
#endif

namespace NStandard;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class ObjectExtensions
{
    /// <summary>
    /// Run a task for the object, then return itself.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="pipe"></param>
    /// <returns></returns>
    public static T Pipe<T>(this T @this, Action<T> pipe)
    {
        pipe(@this);
        return @this;
    }

    /// <summary>
    /// Assign the object to an external variable, then return itself.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="this"></param>
    /// <param name="out"></param>
    /// <returns></returns>
    public static T Pipe<T>(this T @this, out T @out)
    {
        @out = @this;
        return @this;
    }

    /// <summary>
    /// Casts the object to another object through the specified convert method.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TRet"></typeparam>
    /// <param name="this"></param>
    /// <param name="pipe"></param>
    /// <returns></returns>
    public static TRet Pipe<T, TRet>(this T @this, Func<T, TRet> pipe)
    {
        return pipe(@this);
    }

    public static Reflector GetReflector(this object @this) => new(@this.GetType(), @this);
    public static Reflector GetReflector(this object @this, Type type) => new(type, @this);
    public static Reflector GetReflector<T>(this object @this) => new(typeof(T), @this);

#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET451_OR_GREATER
    /// <summary>
    /// Converts the specified object to <see cref="ExpandoObject"/>.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static ExpandoObject ToExpandoObject(this object @this)
    {
        var obj = new ExpandoObject();
        var objDict = obj as IDictionary<string, object>;

        var props = @this.GetType().GetProperties();
        foreach (var prop in props)
        {
            var value = prop.GetValue(@this)!;
            if (value.GetType().IsAnonymousType())
                objDict[prop.Name] = ToExpandoObject(value);
            else objDict[prop.Name] = value;
        }
        return obj;
    }
#endif

}
