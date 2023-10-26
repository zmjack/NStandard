namespace System;

internal static class SR
{
#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
#else
    internal static string Format(string resourceFormat, object p1) => string.Format(resourceFormat, p1);
    internal static string Format(string resourceFormat, object p1, object p2) => string.Format(resourceFormat, p1, p2);
#endif

#if NETCOREAPP3_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
#else
    internal static string @HashCode_HashCodeNotSupported => "HashCode is a mutable struct and should not be compared with other HashCodes. Use ToHashCode to retrieve the computed hash code.";
    internal static string @HashCode_EqualityNotSupported => "HashCode is a mutable struct and should not be compared with other HashCodes.";
#endif

#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET40_OR_GREATER
#else
    internal static string @ArgumentException_TupleIncorrectType => "Argument must be of type {0}.";
    internal static string @ArgumentException_TupleLastArgumentNotATuple => "The last element of an eight element tuple must be a Tuple.";
    internal static string @Argument_EnumTypeDoesNotMatch => "The argument type, '{0}', is not the same as the enum type '{1}'.";
#endif

#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
#else
    internal static string @Cryptography_HashAlgorithmNameNullOrEmpty => "The hash algorithm name cannot be null or empty.";
    internal static string @Cryptography_InvalidHashAlgorithmOid => "The specified OID ({0}) does not represent a known hash algorithm.";

    internal static string @ArgumentOutOfRange_Range => @"Valid values are between {0} and {1}, inclusive.";
#endif
}
