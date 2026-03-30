using System.Linq;

namespace NStandard.Analyzer.Extensions;

public static class PropertyDeclarationSyntaxExtensions
{
    public static bool IsAutoProperty(this Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax @this)
    {
        return @this.AccessorList != null
            && @this.AccessorList.Accessors.All(x => x.Body == null && x.ExpressionBody == null);
    }
}
