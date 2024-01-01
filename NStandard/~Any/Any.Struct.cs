using System;

namespace NStandard;

public static partial class Any
{
    public static class Struct
    {
        /// <summary>
        /// Cast a basic struct to another struct with the same memory sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Cast<T>(object obj) where T : struct
        {
            var bytes = obj switch
            {
                char t => BitConverter.GetBytes(t),
                bool t => BitConverter.GetBytes(t),
                byte t => [t],
                sbyte t => [(byte)t],
                short t => BitConverter.GetBytes(t),
                ushort t => BitConverter.GetBytes(t),
                int t => BitConverter.GetBytes(t),
                uint t => BitConverter.GetBytes(t),
                long t => BitConverter.GetBytes(t),
                ulong t => BitConverter.GetBytes(t),
                float t => BitConverter.GetBytes(t),
                double t => BitConverter.GetBytes(t),
#if NET6_0_OR_GREATER
                Half t => BitConverter.GetBytes(t),
#endif
                _ => throw new NotSupportedException($"Any.Struct.Cast does not support {obj.GetType()}."),
            };

            return typeof(T) switch
            {
                Type t when t == typeof(char) => (T)(BitConverter.ToChar(bytes, 0) as object),
                Type t when t == typeof(bool) => (T)(BitConverter.ToBoolean(bytes, 0) as object),
                Type t when t == typeof(byte) => (T)(bytes[0] as object),
                Type t when t == typeof(sbyte) => (T)((sbyte)bytes[0] as object),
                Type t when t == typeof(short) => (T)(BitConverter.ToInt16(bytes, 0) as object),
                Type t when t == typeof(ushort) => (T)(BitConverter.ToUInt16(bytes, 0) as object),
                Type t when t == typeof(int) => (T)(BitConverter.ToInt32(bytes, 0) as object),
                Type t when t == typeof(uint) => (T)(BitConverter.ToUInt32(bytes, 0) as object),
                Type t when t == typeof(long) => (T)(BitConverter.ToInt64(bytes, 0) as object),
                Type t when t == typeof(ulong) => (T)(BitConverter.ToUInt64(bytes, 0) as object),
                Type t when t == typeof(float) => (T)(BitConverter.ToSingle(bytes, 0) as object),
                Type t when t == typeof(double) => (T)(BitConverter.ToDouble(bytes, 0) as object),
#if NET6_0_OR_GREATER
                Type t when t == typeof(double) => (T)(BitConverter.ToHalf(bytes, 0) as object),
#endif
                _ => throw new NotSupportedException($"Any.Struct.Cast does not support {typeof(T)}."),
            };
        }
    }

}
