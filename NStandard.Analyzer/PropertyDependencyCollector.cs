using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NStandard.Analyzer;

public class PropertyDependencyCollector
{
    public static Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> ReverseDependecies(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies)
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

    public Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> Collect(ClassDeclarationSyntax @class)
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

                if (state == PropertyState.GetAndSet)
                {
                    if (property.Modifiers.Any(x => x.ValueText == "partial"))
                    {
                        dependencies[property] = [];
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

    private IEnumerable<PropertyDeclarationSyntax> Collect(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, StatementSyntax syntax)
    {
        if (syntax is LocalDeclarationStatementSyntax localDeclarationStatementSyntax)
        {
            var declaration = localDeclarationStatementSyntax.Declaration;
            foreach (var variable in declaration.Variables)
            {
                if (variable.Initializer is EqualsValueClauseSyntax equalsValueClauseSyntax)
                {
                    foreach (var target in Collect(dependencies, equalsValueClauseSyntax.Value))
                    {
                        yield return target;
                    }
                }
            }
        }
        else if (syntax is ExpressionStatementSyntax expressionStatementSyntax)
        {
            foreach (var target in Collect(dependencies, expressionStatementSyntax.Expression))
            {
                yield return target;
            }
        }
        else if (syntax is ReturnStatementSyntax returnStatementSyntax)
        {
            foreach (var target in Collect(dependencies, returnStatementSyntax.Expression!))
            {
                yield return target;
            }
        }
        else if (syntax is IfStatementSyntax ifStatementSyntax)
        {
            foreach (var target in Collect(dependencies, ifStatementSyntax.Condition))
            {
                yield return target;
            }
            foreach (var target in Collect(dependencies, ifStatementSyntax.Statement))
            {
                yield return target;
            }
            if (ifStatementSyntax.Else is not null)
            {
                foreach (var target in Collect(dependencies, ifStatementSyntax.Else.Statement))
                {
                    yield return target;
                }
            }
        }
        else if (syntax is BlockSyntax blockSyntax)
        {
            foreach (var statement in blockSyntax.Statements)
            {
                foreach (var target in Collect(dependencies, statement))
                {
                    yield return target;
                }
            }
        }
        else if (syntax is WhileStatementSyntax whileStatementSyntax)
        {
            foreach (var target in Collect(dependencies, whileStatementSyntax.Condition))
            {
                yield return target;
            }
            foreach (var target in Collect(dependencies, whileStatementSyntax.Statement))
            {
                yield return target;
            }
        }
        else if (syntax is ForEachStatementSyntax forEachStatementSyntax)
        {
            foreach (var target in Collect(dependencies, forEachStatementSyntax.Expression))
            {
                yield return target;
            }
            foreach (var target in Collect(dependencies, forEachStatementSyntax.Statement))
            {
                yield return target;
            }
        }
        else if (syntax is ForStatementSyntax forStatementSyntax)
        {
            if (forStatementSyntax.Declaration is not null)
            {
                var declaration = forStatementSyntax.Declaration;
                foreach (var variable in declaration.Variables)
                {
                    if (variable.Initializer is EqualsValueClauseSyntax equalsValueClauseSyntax)
                    {
                        foreach (var target in Collect(dependencies, equalsValueClauseSyntax.Value))
                        {
                            yield return target;
                        }
                    }
                }
            }
            if (forStatementSyntax.Condition is not null)
            {
                foreach (var target in Collect(dependencies, forStatementSyntax.Condition))
                {
                    yield return target;
                }
            }
            foreach (var incrementor in forStatementSyntax.Incrementors)
            {
                foreach (var target in Collect(dependencies, incrementor))
                {
                    yield return target;
                }
            }
            foreach (var target in Collect(dependencies, forStatementSyntax.Statement))
            {
                yield return target;
            }
        }
        else if (syntax is SwitchStatementSyntax switchStatementSyntax)
        {
            foreach (var target in Collect(dependencies, switchStatementSyntax.Expression))
            {
                yield return target;
            }
            foreach (var section in switchStatementSyntax.Sections)
            {
                foreach (var statement in section.Statements)
                {
                    foreach (var target in Collect(dependencies, statement))
                    {
                        yield return target;
                    }
                }
            }
        }
        else if (syntax is TryStatementSyntax tryStatementSyntax)
        {
            foreach (var target in Collect(dependencies, tryStatementSyntax.Block))
            {
                yield return target;
            }
            foreach (var catchClause in tryStatementSyntax.Catches)
            {
                foreach (var target in Collect(dependencies, catchClause.Block))
                {
                    yield return target;
                }
            }
            if (tryStatementSyntax.Finally is not null)
            {
                foreach (var target in Collect(dependencies, tryStatementSyntax.Finally.Block))
                {
                    yield return target;
                }
            }
        }
        else if (syntax is DoStatementSyntax doStatementSyntax)
        {
            foreach (var target in Collect(dependencies, doStatementSyntax.Condition))
            {
                yield return target;
            }
            foreach (var target in Collect(dependencies, doStatementSyntax.Statement))
            {
                yield return target;
            }
        }
    }

    private IEnumerable<PropertyDeclarationSyntax> Collect(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, ExpressionSyntax syntax)
    {
        // Map. BreakPoint set here while debuging.
        if (syntax is IdentifierNameSyntax nameSyntax)
        {
            foreach (var target in CollectCore(dependencies, nameSyntax))
            {
                yield return target;
            }
        }
        else if (syntax is InvocationExpressionSyntax invocationSyntax)
        {
            foreach (var target in CollectCore(dependencies, invocationSyntax))
            {
                yield return target;
            }
        }
        else if (syntax is InterpolatedStringExpressionSyntax interpolatedStringSyntax)
        {
            foreach (var target in CollectCore(dependencies, interpolatedStringSyntax))
            {
                yield return target;
            }
        }
        else if (syntax is BinaryExpressionSyntax binarySyntax)
        {
            foreach (var target in CollectCore(dependencies, binarySyntax))
            {
                yield return target;
            }
        }
        else if (syntax is PrefixUnaryExpressionSyntax prefixUnarySyntax)
        {
            foreach (var target in CollectCore(dependencies, prefixUnarySyntax))
            {
                yield return target;
            }
        }
        else if (syntax is ConditionalExpressionSyntax conditionalSyntax)
        {
            foreach (var target in CollectCore(dependencies, conditionalSyntax))
            {
                yield return target;
            }
        }
        else if (syntax is BaseObjectCreationExpressionSyntax baseObjectCreationSyntax)
        {
            foreach (var target in CollectCore(dependencies, baseObjectCreationSyntax))
            {
                yield return target;
            }
        }
        else if (syntax is ParenthesizedExpressionSyntax parenthesizedSyntax)
        {
            foreach (var target in CollectCore(dependencies, parenthesizedSyntax))
            {
                yield return target;
            }
        }
        else if (syntax is IsPatternExpressionSyntax isPatternSyntax)
        {
            foreach (var target in CollectCore(dependencies, isPatternSyntax))
            {
                yield return target;
            }
        }
        else if (syntax is LambdaExpressionSyntax lambdaSyntax)
        {
            if (lambdaSyntax.Body is BlockSyntax block)
            {
                foreach (var target in Collect(dependencies, block))
                {
                    yield return target;
                }
            }
            else if (lambdaSyntax.Body is ExpressionSyntax expression)
            {
                foreach (var target in Collect(dependencies, expression))
                {
                    yield return target;
                }
            }
        }
    }

    private IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, IdentifierNameSyntax syntax)
    {
        var reference = dependencies.Keys.FirstOrDefault(x => x.Identifier.ValueText == syntax.Identifier.ValueText);
        if (reference is not null)
        {
            yield return reference;
        }
    }

    private IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, InvocationExpressionSyntax syntax)
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

    private IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, InterpolatedStringExpressionSyntax syntax)
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

    private IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, BinaryExpressionSyntax syntax)
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

    private IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, PrefixUnaryExpressionSyntax syntax)
    {
        foreach (var target in Collect(dependencies, syntax.Operand))
        {
            yield return target;
        }
    }

    private IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, ConditionalExpressionSyntax syntax)
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

    private IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, BaseObjectCreationExpressionSyntax syntax)
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

    private IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, ParenthesizedExpressionSyntax syntax)
    {
        foreach (var target in Collect(dependencies, syntax.Expression))
        {
            yield return target;
        }
    }

    private IEnumerable<PropertyDeclarationSyntax> CollectCore(Dictionary<PropertyDeclarationSyntax, ICollection<PropertyDeclarationSyntax>> dependencies, IsPatternExpressionSyntax syntax)
    {
        foreach (var target in Collect(dependencies, syntax.Expression))
        {
            yield return target;
        }
    }
}
