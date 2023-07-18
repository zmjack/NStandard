using System.Reflection;

namespace NStandard
{
    public static class TypeEx
    {
        public const BindingFlags DeclaredOnlyLookup = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
    }

}
