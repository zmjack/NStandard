using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
#if NETSTANDARD2_0
using System.Reflection;
using System.Dynamic;
#endif

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XObject
    {
        /// <summary>
        /// Do a task for itself, then return itself.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static TSelf Then<TSelf>(this TSelf @this, Action<TSelf> task)
        {
            task(@this);
            return @this;
        }

        /// <summary>
        /// Casts the element to the specified type through the specified convert method.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="convert"></param>
        /// <returns></returns>
        public static TRet For<TSelf, TRet>(this TSelf @this, TRet convert) => convert;

        /// <summary>
        /// Casts the element to the specified type through the specified convert method.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="convert"></param>
        /// <returns></returns>
        public static TRet For<TSelf, TRet>(this TSelf @this, Func<TSelf, TRet> convert) => convert(@this);

        /// <summary>
        /// Casts the element to the specified type through the specified convert method.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="convert"></param>
        /// <returns></returns>
        public static TSelf NFor<TSelf>(this TSelf @this, Func<TSelf, TSelf> convert, int degree = 1)
        {
            var param = @this;
            for (int i = 0; i < degree; i++)
                param = convert(param);
            return param;
        }

        /// <summary>
        /// Casts the element to the specified type through the specified flow.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="convert"></param>
        /// <returns></returns>
        public static TRet Flow<T, TRet>(this T @this, IFlow<T, TRet> flow) => flow.Execute(@this);

        /// <summary>
        /// Casts the element to the specified type through the specified filter method.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        [Obsolete("Replace with the new function: TRet For<TSelf, TRet>(this TSelf @this, Func<TSelf, TRet> convert)", true)]
        public static TRet For<TSelf, TRet>(this TSelf @this, Func<TSelf, TRet>[] filters)
            where TRet : class
        {
            foreach (var project in filters)
            {
                var result = project(@this);
                if (!(result is null))
                    return result;
            }
            return null;
        }

        /// <summary>
        /// Casts the element to the specified type through the specified convert method.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static TRet Return<TSelf, TRet>(this TSelf @this, Func<TSelf, TRet> onNormalReturn, Func<TSelf, TRet> @default)
        {
            try { return onNormalReturn(@this); }
            catch { return @default(@this); }
        }

        /// <summary>
        /// Casts the element to the specified type through the specified convert method.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static TRet Return<TSelf, TRet>(this TSelf @this, Func<TSelf, TRet> onNormalReturn, TRet @default)
        {
            try { return onNormalReturn(@this); }
            catch { return @default; }
        }

        /// <summary>
        /// Determines whether the specified element in a sequence by using the default equality comparer.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static bool In<TSource>(this TSource @this, params TSource[] sequence)
            => sequence.Contains(@this);

        /// <summary>
        /// Determines whether the specified element in a sequence by using the default equality comparer.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="this"></param>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public static bool In<TSource>(this TSource @this, IEnumerable<TSource> sequence)
            => sequence.Contains(@this);

        /// <summary>
        /// Determines whether the specified object is null.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNull<TSelf>(this TSelf @this) where TSelf : class => @this is null;

        public static void Dump<TSelf>(this TSelf @this) => Dump(@this, Console.Out);
        public static void Dump<TSelf>(this TSelf @this, TextWriter writer)
        {
            writer.WriteLine($"<{typeof(TSelf).GetSimplifiedName()}>");
            Dump(@this, writer, null, 0);
        }

        private static void Dump(object instance, TextWriter writer, string name, int paddingLeft)
        {
            var type = instance?.GetType();
            switch (type)
            {
                case null:
                    if (name is null)
                        writer.WriteLine($"{" ".Repeat(paddingLeft)}<null>{instance}");
                    else if (name == string.Empty)
                        writer.WriteLine($"{" ".Repeat(paddingLeft)}<null>{instance},");
                    else writer.WriteLine($"{" ".Repeat(paddingLeft)}{name}: <null>{instance},");
                    break;

                case Type _ when type.IsBasicType():
                    if (name is null)
                        writer.WriteLine($"{" ".Repeat(paddingLeft)}<{type.GetSimplifiedName()}>{instance}");
                    else if (name == string.Empty)
                        writer.WriteLine($"{" ".Repeat(paddingLeft)}<{type.GetSimplifiedName()}>{instance},");
                    else writer.WriteLine($"{" ".Repeat(paddingLeft)}{name}: <{type.GetSimplifiedName()}>{instance},");
                    break;

                case Type _ when type.IsExtend<Array>():
                    writer.WriteLine($"{" ".Repeat(paddingLeft)}[");
                    var enumerator = (instance as IEnumerable).GetEnumerator();
                    for (var element = enumerator.TakeElement(); element != null; element = enumerator.TakeElement())
                    {
                        Dump(element, writer, string.Empty, paddingLeft + 4);
                    }
                    writer.WriteLine($"{" ".Repeat(paddingLeft)}]");
                    break;

                default:
                    writer.WriteLine($"{" ".Repeat(paddingLeft)}{{");
                    var props = type.GetProperties();
                    foreach (var prop in props)
                    {
                        Dump(prop.GetValue(instance, null), writer, prop.Name, paddingLeft + 4);
                    }
                    writer.WriteLine($"{" ".Repeat(paddingLeft)}}},");
                    break;
            }
        }

        public static Reflector GetReflector(this object @this) => new Reflector(@this, @this.GetType());
        public static Reflector GetReflector(this object @this, Type type) => new Reflector(@this, type);
        public static Reflector GetReflector<T>(this T @this) => new Reflector(@this, typeof(T));


#if NETSTANDARD2_0
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
                var value = prop.GetValue(@this);
                if (value.GetType().IsAnonymousType())
                    objDict[prop.Name] = ToExpandoObject(value);
                else objDict[prop.Name] = value;
            }
            return obj;
        }
#endif

    }
}
