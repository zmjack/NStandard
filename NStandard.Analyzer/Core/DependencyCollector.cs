using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace NStandard.Analyzer.Core;

internal abstract class DependencyCollector<TTarget, TDependencies, TElement>
    where TTarget : class
    where TDependencies : class
    where TElement : class
{
    public abstract TDependencies Collect(SemanticModel semantic, TTarget @class);

    protected IEnumerable<TElement> Collect(TDependencies dependencies, StatementSyntax syntax)
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

    protected IEnumerable<TElement> Collect(TDependencies dependencies, ExpressionSyntax syntax)
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

    protected abstract IEnumerable<TElement> CollectCore(TDependencies dependencies, IdentifierNameSyntax syntax);
    protected abstract IEnumerable<TElement> CollectCore(TDependencies dependencies, InvocationExpressionSyntax syntax);
    protected abstract IEnumerable<TElement> CollectCore(TDependencies dependencies, InterpolatedStringExpressionSyntax syntax);
    protected abstract IEnumerable<TElement> CollectCore(TDependencies dependencies, BinaryExpressionSyntax syntax);
    protected abstract IEnumerable<TElement> CollectCore(TDependencies dependencies, PrefixUnaryExpressionSyntax syntax);
    protected abstract IEnumerable<TElement> CollectCore(TDependencies dependencies, ConditionalExpressionSyntax syntax);
    protected abstract IEnumerable<TElement> CollectCore(TDependencies dependencies, BaseObjectCreationExpressionSyntax syntax);
    protected abstract IEnumerable<TElement> CollectCore(TDependencies dependencies, ParenthesizedExpressionSyntax syntax);
    protected abstract IEnumerable<TElement> CollectCore(TDependencies dependencies, IsPatternExpressionSyntax syntax);
}
