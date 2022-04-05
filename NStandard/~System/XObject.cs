using System;
using System.Collections.Generic;
using System.ComponentModel;
#if NETSTANDARD2_0_OR_GREATER
using System.Reflection;
using System.Dynamic;
#endif

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XObject
    {
        /// <summary>
        /// Run a task for the object, then return itself.
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
        /// Run a task for the object, then return itself.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public static TSelf Then<TSelf>(this TSelf @this, Action task)
        {
            task();
            return @this;
        }

        /// <summary>
        /// Casts the object to another object through the specified convert method.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="convert"></param>
        /// <returns></returns>
        public static TRet For<TSelf, TRet>(this TSelf @this, TRet convert) => convert;

        /// <summary>
        /// Casts the object to another object through the specified convert method.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="convert"></param>
        /// <returns></returns>
        public static TRet For<TSelf, TRet>(this TSelf @this, Func<TSelf, TRet> convert) => convert(@this);

        /// <summary>
        /// Casts the object to another object through the specified convert method.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <typeparam name="TParam"></typeparam>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="this"></param>
        /// <param name="param"></param>
        /// <param name="convert"></param>
        /// <returns></returns>
        public static TRet For<TSelf, TParam, TRet>(this TSelf @this, Func<TSelf, TParam, TRet> convert, TParam param) => convert(@this, param);

        /// <summary>
        /// Calculate the element by path and return the element that meet the stop condition.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="forward"></param>
        /// <param name="stopCondition"></param>
        /// <returns></returns>
        public static TSelf Forward<TSelf>(this TSelf @this, Func<TSelf, TSelf> forward, Func<TSelf, bool> stopCondition)
        {
            var current = @this;
            while (!stopCondition(current)) current = forward(current);
            return current;
        }

        /// <summary>
        /// Calculate the element by path and return the element that meet the stop condition.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="forward"></param>
        /// <param name="stopCondition"></param>
        /// <returns></returns>
        public static TSelf Forward<TSelf>(this TSelf @this, Func<TSelf, TSelf> forward, Func<TSelf, int, bool> stopCondition)
        {
            var current = @this;
            for (int degree = 0; !stopCondition(current, degree); degree++) current = forward(current);
            return current;
        }

        /// <summary>
        /// Calculate the element by path and return the element that meet the stop condition.
        /// </summary>
        /// <typeparam name="TSelf"></typeparam>
        /// <param name="this"></param>
        /// <param name="forward"></param>
        /// <param name="degree"></param>
        /// <returns></returns>
        public static TSelf Forward<TSelf>(this TSelf @this, Func<TSelf, TSelf> forward, int degree)
        {
            if (degree < 0) throw new ArgumentException("The degree must be nonnegative.", nameof(degree));
            var current = @this;
            for (int i = 0; i < degree; i++) current = forward(current);
            return current;
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
        public static T MemoryAs<T>(this object @this) where T : struct => (T)MemoryAs(@this, typeof(T));

        /// <summary>
        /// Convert a basic struct to another basic struct with same memory sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object MemoryAs(this object @this, Type type)
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

            return type switch
            {
                Type t when t == typeof(char) => BitConverter.ToChar(bytes, 0),
                Type t when t == typeof(bool) => BitConverter.ToBoolean(bytes, 0),
                Type t when t == typeof(byte) => bytes[0],
                Type t when t == typeof(sbyte) => (sbyte)bytes[0],
                Type t when t == typeof(short) => BitConverter.ToInt16(bytes, 0),
                Type t when t == typeof(ushort) => BitConverter.ToUInt16(bytes, 0),
                Type t when t == typeof(int) => BitConverter.ToInt32(bytes, 0),
                Type t when t == typeof(uint) => BitConverter.ToUInt32(bytes, 0),
                Type t when t == typeof(long) => BitConverter.ToInt64(bytes, 0),
                Type t when t == typeof(ulong) => BitConverter.ToUInt64(bytes, 0),
                Type t when t == typeof(float) => BitConverter.ToSingle(bytes, 0),
                Type t when t == typeof(double) => BitConverter.ToDouble(bytes, 0),
                _ => throw new NotSupportedException(),
            };
        }

        public static Reflector GetReflector(this object @this) => new(@this.GetType(), @this);
        public static Reflector GetReflector(this object @this, Type type) => new(type, @this);
        public static Reflector GetReflector<T>(this object @this) => new(typeof(T), @this);

#if NETSTANDARD2_0_OR_GREATER
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
