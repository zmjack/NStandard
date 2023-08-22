using System;
using System.Text;

namespace NStandard
{
    public static partial class ConvertEx
    {
        /// <summary>
        /// Returns an object of the specified type and whose value is equivalent to the specified object.
        ///     (Enhance: Support Nullable types.)
        /// </summary>
        /// <param name="value">An object that implements the System.IConvertible interface.</param>
        /// <param name="conversionType">The type of object to return.</param>
        public static object ChangeType(object value, Type conversionType)
        {
            if (conversionType.IsNullable())
            {
                if (value is null) return null;
                else return Convert.ChangeType(value, conversionType.GetGenericArguments()[0]);
            }
            else
            {
                if (value is null) return conversionType.CreateDefault();
                else return Convert.ChangeType(value, conversionType);
            }
        }

        public static string ToBase58String(byte[] bytes) => Base58Converter.ToBase58String(bytes);
        public static byte[] FromBase58String(string str) => Base58Converter.FromBase58String(str);

        //TODO: Functions are being designed.
        //public static string ToBase32String(byte[] bytes) => Base32Converter.ToBase32String(bytes);
        //public static byte[] FromBase32String(string str) => Base32Converter.FromBase32String(str);
    }
}
