namespace System
{
    internal static class SR
    {
#if NET35
        internal const string ArgumentException_TupleIncorrectType = "Argument must be of type {0}.";
        internal const string ArgumentException_TupleLastArgumentNotATuple = "The last element of an eight element tuple must be a Tuple.";
        internal const string Argument_EnumTypeDoesNotMatch = "The argument type, '{0}', is not the same as the enum type '{1}'.";
#endif

#if NET35 || NET40 || NET45 || NET451 || NET452
        internal const string Cryptography_HashAlgorithmNameNullOrEmpty = "The hash algorithm name cannot be null or empty.";
        internal const string Cryptography_InvalidHashAlgorithmOid = "The specified OID ({0}) does not represent a known hash algorithm.";

        internal static string Format(string resourceFormat, object p1) => string.Format(resourceFormat, p1);
        internal static string Format(string resourceFormat, object p1, object p2) => string.Format(resourceFormat, p1, p2);

        internal const string ArgumentOutOfRange_Range = @"Valid values are between {0} and {1}, inclusive.";
#endif

    }
}
