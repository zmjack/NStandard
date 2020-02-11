#if NET35
namespace System
{
    internal static class SR
    {
        internal const string ArgumentException_TupleIncorrectType = "Argument must be of type {0}.";
        internal const string ArgumentException_TupleLastArgumentNotATuple = "The last element of an eight element tuple must be a Tuple.";
        internal const string Argument_EnumTypeDoesNotMatch = "The argument type, '{0}', is not the same as the enum type '{1}'.";

        internal static string Format(string resourceFormat, object p1) => string.Format(resourceFormat, p1);
        internal static string Format(string resourceFormat, object p1, object p2) => string.Format(resourceFormat, p1, p2);
    }
}
#endif
