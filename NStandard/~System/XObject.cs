using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="convert"></param>
        /// <returns></returns>
        public static TSelf ForUntil<TSelf>(this TSelf @this, Func<TSelf, TSelf> convert, Func<TSelf, bool> until)
        {
            var ret = @this;
            while (!until(ret))
                ret = convert(ret);
            return ret;
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
        /// Determines whether the specified object is null.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNull<TSelf>(this TSelf @this) where TSelf : class => @this is null;

        /// <summary>
        /// Convert a basic struct to another basic struct with same memory sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T MemoryAs<T>(this object @this) where T : struct => (T)MemoryAS(@this, typeof(T));

        /// <summary>
        /// Convert a basic struct to another basic struct with same memory sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static object MemoryAS(this object @this, Type type)
        {
            if (@this.GetType() == type) return @this;

            var bytes = @this switch
            {
                char t => BitConverter.GetBytes(t),
                bool t => BitConverter.GetBytes(t),
                byte t => BitConverter.GetBytes(t),
                sbyte t => BitConverter.GetBytes(t),
                short t => BitConverter.GetBytes(t),
                ushort t => BitConverter.GetBytes(t),
                int t => BitConverter.GetBytes(t),
                uint t => BitConverter.GetBytes(t),
                long t => BitConverter.GetBytes(t),
                ulong t => BitConverter.GetBytes(t),
                float t => BitConverter.GetBytes(t),
                double t => BitConverter.GetBytes(t),
                _ => throw new NotSupportedException(),
            };

            switch (type)
            {
                case Type t when t == typeof(char): return BitConverter.ToChar(bytes, 0);
                case Type t when t == typeof(bool): return BitConverter.ToBoolean(bytes, 0);
                case Type t when t == typeof(byte): return bytes[0];
                case Type t when t == typeof(sbyte): return (sbyte)bytes[0];
                case Type t when t == typeof(short): return BitConverter.ToInt16(bytes, 0);
                case Type t when t == typeof(ushort): return BitConverter.ToUInt16(bytes, 0);
                case Type t when t == typeof(int): return BitConverter.ToInt32(bytes, 0);
                case Type t when t == typeof(uint): return BitConverter.ToUInt32(bytes, 0);
                case Type t when t == typeof(long): return BitConverter.ToInt64(bytes, 0);
                case Type t when t == typeof(ulong): return BitConverter.ToUInt64(bytes, 0);
                case Type t when t == typeof(float): return BitConverter.ToSingle(bytes, 0);
                case Type t when t == typeof(double): return BitConverter.ToDouble(bytes, 0);
                default: throw new NotSupportedException();
            };
        }

        public static Reflector GetReflector(this object @this) => new Reflector(@this.GetType(), @this);
        public static Reflector GetReflector(this object @this, Type type) => new Reflector(type, @this);
        public static Reflector GetReflector<T>(this object @this) => new Reflector(typeof(T), @this);

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
