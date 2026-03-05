using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NStandard.Analyzer.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NStandard.Analyzer;

internal class PropertyDependencyCollector : DependencyCollector<ClassDeclarationSyntax, Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>>, PropertyDeclarationSyntax>
{
    [Flags]
    private enum PropertyState
    {
        None,
        Get = 0b_0001,
        GetBody = 0b_0010,
        GetExpBody = 0b_0100,
        Set = 0b_0001_0000,
        SetBody = 0b_0010_0000,
        SetExpBody = 0b_0100_0000,
        GetAndSet = Get | Set,
    }

    public override Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> Collect(SemanticModel semantic, ClassDeclarationSyntax @class)
    {
        var dependencies = new Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>>();
        var properties = @class.DescendantNodes().OfType<PropertyDeclarationSyntax>();
        foreach (var property in properties)
        {
            if (property.ExpressionBody is null)
            {
                PropertyState state = PropertyState.None;
                foreach (var accessor in property.AccessorList!.Accessors)
                {
                    if (accessor.Body is not null)
                    {
                        if (accessor.Keyword.ValueText == "get")
                        {
                            state |= PropertyState.GetBody;
                        }
                        else if (accessor.Keyword.ValueText == "set")
                        {
                            state |= PropertyState.SetBody;
                        }
                    }
                    else if (accessor.ExpressionBody is not null)
                    {
                        if (accessor.Keyword.ValueText == "get")
                        {
                            state |= PropertyState.GetExpBody;
                        }
                        else if (accessor.Keyword.ValueText == "set")
                        {
                            state |= PropertyState.SetExpBody;
                        }
                    }
                    else
                    {
                        if (accessor.Keyword.ValueText == "get")
                        {
                            state |= PropertyState.Get;
                        }
                        else if (accessor.Keyword.ValueText == "set")
                        {
                            state |= PropertyState.Set;
                        }
                    }
                }

                if (state is PropertyState.GetAndSet)
                {
                    bool markDependencyProperty = false;
                    var attributes = property.AttributeLists.SelectMany(x => x.Attributes);
                    foreach (var attr in attributes)
                    {
                        var attrType = semantic.GetTypeInfo(attr);
                        if (attrType.ConvertedType!.ToString() == "NStandard.ComponentModel.DependencyPropertyAttribute")
                        {
                            markDependencyProperty = true;
                            break;
                        }
                    }

                    if (!markDependencyProperty)
                    {
                        bool hasPartial = false, hasStatic = false;
                        foreach (var modifier in property.Modifiers)
                        {
                            if (modifier.ValueText == "partial")
                            {
                                hasPartial = true;
                            }
                            else if (modifier.ValueText == "static")
                            {
                                hasStatic = true;
                            }
                        }

                        if (!hasStatic && hasPartial)
                        {
                            dependencies[property] = [];
                        }
                    }
                }
                else if (state.HasFlag(PropertyState.GetBody))
                {
                    var block = property.AccessorList!.Accessors.First(x => x.Keyword.ValueText == "get").Body!;
                    dependencies[property] = new HashSet<PropertyDeclarationSyntax>(block.Statements.SelectMany(s => Collect(dependencies, s)));
                }
                else if (state.HasFlag(PropertyState.GetExpBody))
                {
                    var arrow = property.AccessorList!.Accessors.First(x => x.Keyword.ValueText == "get").ExpressionBody!;
                    dependencies[property] = new HashSet<PropertyDeclarationSyntax>(Collect(dependencies, arrow.Expression));
                }
            }
            else
            {
                if (property.ExpressionBody is ArrowExpressionClauseSyntax arrow)
                {
                    dependencies[property] = new HashSet<PropertyDeclarationSyntax>(Collect(dependencies, arrow.Expression));
                }
            }
        }

        return dependencies;
    }

    protected override IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, IdentifierNameSyntax syntax)
    {
        var reference = dependencies.Keys.FirstOrDefault(x => x.Identifier.ValueText == syntax.Identifier.ValueText);
        if (reference is not null)
        {
            yield return reference;
        }
    }

    protected override IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, InvocationExpressionSyntax syntax)
    {
        var arguments = syntax.ArgumentList.Arguments;
        foreach (var argument in arguments)
        {
            foreach (var target in Collect(dependencies, argument.Expression))
            {
                yield return target;
            }
        }
    }

    protected override IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, InterpolatedStringExpressionSyntax syntax)
    {
        var contents = syntax.Contents;
        var interrpolationSyntaxes = syntax.Contents.OfType<InterpolationSyntax>();

        foreach (var interrpolationSyntax in interrpolationSyntaxes)
        {
            foreach (var target in Collect(dependencies, interrpolationSyntax.Expression))
            {
                yield return target;
            }
        }
    }

    protected override IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, BinaryExpressionSyntax syntax)
    {
        foreach (var target in Collect(dependencies, syntax.Left))
        {
            yield return target;
        }
        foreach (var target in Collect(dependencies, syntax.Right))
        {
            yield return target;
        }
    }

    protected override IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, PrefixUnaryExpressionSyntax syntax)
    {
        foreach (var target in Collect(dependencies, syntax.Operand))
        {
            yield return target;
        }
    }

    protected override IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, ConditionalExpressionSyntax syntax)
    {
        foreach (var target in Collect(dependencies, syntax.Condition))
        {
            yield return target;
        }
        foreach (var target in Collect(dependencies, syntax.WhenTrue))
        {
            yield return target;
        }
        foreach (var target in Collect(dependencies, syntax.WhenFalse))
        {
            yield return target;
        }
    }

    protected override IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, BaseObjectCreationExpressionSyntax syntax)
    {
        var arguments = syntax.ArgumentList!.Arguments;
        foreach (var argument in arguments)
        {
            foreach (var target in Collect(dependencies, argument.Expression))
            {
                yield return target;
            }
        }
    }

    protected override IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, ParenthesizedExpressionSyntax syntax)
    {
        foreach (var target in Collect(dependencies, syntax.Expression))
        {
            yield return target;
        }
    }

    protected override IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, IsPatternExpressionSyntax syntax)
    {
        foreach (var target in Collect(dependencies, syntax.Expression))
        {
            yield return target;
        }
    }

    public Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> ReverseDependecies(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies)
    {
        IEnumerable<PropertyDeclarationSyntax> GetRootReferences(IEnumerable<PropertyDeclarationSyntax> references)
        {
            foreach (var reference in references)
            {
                if (dependencies.TryGetValue(reference, out var dependency))
                {
                    if (!dependency.Any())
                    {
                        yield return reference;
                    }
                }
                else
                {
                    foreach (var root in GetRootReferences(dependencies[reference]))
                    {
                        yield return root;
                    }
                }
            }
        }

        var list = new Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>>();
        foreach (var dependency in dependencies)
        {
            if (dependency.Key.ExpressionBody is null)
            {
                foreach (var accessor in dependency.Key.AccessorList!.Accessors)
                {
                    if (accessor.Keyword.ValueText == "set" && accessor.Body is null && accessor.ExpressionBody is null)
                    {
                        if (!list.ContainsKey(dependency.Key))
                        {
                            list[dependency.Key] = new HashSet<PropertyDeclarationSyntax>();
                            break;
                        }
                    }
                }
            }

            if (dependency.Value.Any())
            {
                foreach (var reference in GetRootReferences(dependency.Value))
                {
                    if (!list.ContainsKey(reference))
                    {
                        list[reference] = new HashSet<PropertyDeclarationSyntax>();
                    }
                    list[reference].Add(dependency.Key);
                }
            }
        }
        return list;
    }

}
