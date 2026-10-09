using Microsoft.CodeAnalysis;

namespace NStandard.Analyzer.Extensions;

internal static class SyntaxTokenListExtensions
{
    public static bool ContainsToken(this SyntaxTokenList @this, SyntaxModifier modifier)
    {
        foreach (var token in @this)
        {
            if (token.Text == modifier.ToString())
            {
                return true;
            }
        }
        return false;
    }
}
