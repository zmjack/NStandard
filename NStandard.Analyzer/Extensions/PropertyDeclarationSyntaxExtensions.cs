using System.Linq;

namespace NStandard.Analyzer.Extensions;

internal static class PropertyDeclarationSyntaxExtensions
{
    internal static bool IsAutoProperty(this Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax @this)
    {
        return @this.AccessorList != null
            && @this.AccessorList.Accessors.All(x => x.Body == null && x.ExpressionBody == null);
    }
}
