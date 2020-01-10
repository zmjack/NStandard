using System;

namespace NStd
{
    public class ConvertEx
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
                if (value is null)
                    return null;
                else return Convert.ChangeType(value, conversionType.GetGenericArguments()[0]);
            }
            else return Convert.ChangeType(value, conversionType);
        }

    }
}
