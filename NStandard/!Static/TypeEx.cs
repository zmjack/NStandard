using System.Reflection;

namespace NStandard.Static;

public static class TypeEx
{
    public const BindingFlags DeclaredOnlyLookup = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
}
