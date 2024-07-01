using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace NStandard.Evaluators;

public static class Evaluator
{
    public static readonly NumericalEvaluator Numerical = new(true);
}

public abstract partial class EvaluatorBase
{
    private static readonly MethodInfo BracketMethod = typeof(EvaluatorBase).GetDeclaredMethod(nameof(Bracket))!;
    private static readonly MethodInfo DictionaryGetItemMethod = typeof(IDictionary<,>).MakeGenericType(typeof(string), typeof(double)).GetMethod("get_Item")!;
    private static readonly MethodInfo DictionaryContainsKeyMethod = typeof(IDictionary<,>).MakeGenericType(typeof(string), typeof(double)).GetMethod("ContainsKey")!;

#if NET8_0_OR_GREATER
    [GeneratedRegex(@"([\[\]\-\.\^\$\{\}\?\+\*\|\(\)])")]
    private static partial Regex GetNormalRegex();
    private static readonly Regex NormalRegex = GetNormalRegex();
#else
    private static readonly Regex NormalRegex = new(@"([\[\]\-\.\^\$\{\}\?\+\*\|\(\)])");
#endif

    protected abstract string DefaultExpression { get; }
    protected abstract string OperandRegexString { get; }
    protected abstract Expression OperandToExpression(string operand);
    protected abstract string ParameterRegexString { get; }
    protected abstract Func<string, string> GetParameterName { get; }

    protected abstract Dictionary<string, int> BinaryOpLevels { get; }
    protected abstract Dictionary<string, UnaryFunc<Expression>?> UnaryOpFunctions { get; }
    protected abstract Dictionary<string, BinaryFunc<Expression>> BinaryOpFunctions { get; }

    protected abstract Dictionary<Bracket, UnaryFunc<double>?> BracketFunctions { get; }

    protected string[]? UnaryOperators { get; private set; }
    protected string[]? BinaryOperators { get; private set; }
    protected string[]? StartBrackets { get; private set; }
    protected string[]? EndBrackets { get; private set; }
    protected Dictionary<string, string[]>? StartBracketMap { get; private set; }
    protected IEnumerable<Node>? PendingNodes { get; private set; }
    protected Regex? OperandRegex { get; private set; }
    protected Regex? ParameterRegex { get; private set; }
    protected Regex? ResolveRegex { get; private set; }

    public bool Initialized { get; }

    public EvaluatorBase()
    {
        var undefinedOps = BinaryOpLevels.Keys.Where(x => !BinaryOpFunctions.ContainsKey(x));
        if (undefinedOps.Any()) throw new ArgumentException($"Some operators are undefined. ({undefinedOps.Join(",")})");

        var undefinedLevels = BinaryOpFunctions.Keys.Where(x => !BinaryOpLevels.ContainsKey(x));
        if (undefinedLevels.Any()) throw new ArgumentException($"Some operators are undefined. ({undefinedLevels.Join(",")})");
    }
    public EvaluatorBase(bool autoInitialize) : this()
    {
        if (autoInitialize) Initialize();
    }

    protected void Initialize()
    {
        UnaryOperators = [.. UnaryOpFunctions.Keys];
        BinaryOperators = [.. BinaryOpFunctions.Keys];
        StartBrackets = BracketFunctions.Select(x => x.Key.Start).Distinct().ToArray();
        EndBrackets = BracketFunctions.Select(x => x.Key.End).Distinct().ToArray();
        StartBracketMap = BracketFunctions
            .Select(x => x.Key).GroupBy(x => x.End)
            .ToDictionary(x => x.Key, x => x.Select(i => i.Start)
            .ToArray());

        OperandRegex = new Regex(OperandRegexString, RegexOptions.Compiled);
        ParameterRegex = new Regex(ParameterRegexString, RegexOptions.Compiled);

        var sb = new StringBuilder();
        sb.Append($@"^\s*(?:({OperandRegexString}|{ParameterRegexString}");
        foreach (var s in UnaryOperators) sb.Append($"|{NormalRegexString(s)}");
        foreach (var s in BinaryOperators) sb.Append($"|{NormalRegexString(s)}");
        foreach (var s in StartBrackets) sb.Append($"|{NormalRegexString(s)}");
        foreach (var s in EndBrackets) sb.Append($"|{NormalRegexString(s)}");
        sb.Append(@")+\s*)+?\s*$");
        ResolveRegex = new(sb.ToString());

        IEnumerable<Node> GetPendingNodes()
        {
            foreach (var value in UnaryOperators) yield return new Node(NodeType.UnaryOperator, value);
            foreach (var value in BinaryOperators) yield return new Node(NodeType.BinaryOperator, value);
            foreach (var value in StartBrackets) yield return new Node(NodeType.StartBracket, value);
            foreach (var value in EndBrackets) yield return new Node(NodeType.EndBracket, value);
        }
        PendingNodes = GetPendingNodes().OrderByDescending(x => x.Value!.Length);
    }

    protected void InitializeIfNot()
    {
        if (!Initialized) Initialize();
    }

    protected static string NormalRegexString(string origin) => origin.RegexReplace(NormalRegex, "\\$1");

    protected static Expression ParameterToExpression(string name, Expression? parameter, Type? parameterType)
    {
        if (parameter == null) throw new ArgumentException($"No dictionary or object found.");

        if (parameterType == typeof(IDictionary<string, double>))
        {
            return
                Expression.Condition(
                    Expression.Call(parameter, DictionaryContainsKeyMethod, Expression.Constant(name)),
                    Expression.Call(parameter, DictionaryGetItemMethod, Expression.Constant(name)),
                    Expression.Constant(0d));
        }
        else
        {
            var propertyOrField = Expression.PropertyOrField(parameter, name);
            if (propertyOrField.Type == typeof(double)) return propertyOrField;
            if (propertyOrField.Type == typeof(bool))
            {
                return
                    Expression.Condition(
                        Expression.Equal(propertyOrField, Expression.Constant(true)),
                        Expression.Constant(1d), Expression.Constant(0d));
            }
            else if (propertyOrField.Type == typeof(bool?))
            {
                return
                    Expression.Condition(
                        Expression.Equal(propertyOrField, Expression.Constant(null)),
                        Expression.Constant(double.NaN),
                        Expression.Condition(
                            Expression.Equal(Expression.Convert(propertyOrField, typeof(bool)), Expression.Constant(true)),
                            Expression.Constant(1d), Expression.Constant(0d)));
            }
            else
            {
                if (propertyOrField.Type.IsNullable())
                {
                    return
                        Expression.Condition(
                            Expression.Equal(propertyOrField, Expression.Constant(null)),
                            Expression.Constant(double.NaN),
                            Expression.Convert(propertyOrField, typeof(double)));
                }
                else return Expression.Convert(propertyOrField, typeof(double));
            }
        }
    }

    protected Dictionary<NodeType, NodeType[]> FollowTypes = new()
    {
        [NodeType.Unspecified] = [NodeType.Operand, NodeType.Parameter, NodeType.UnaryOperator, NodeType.StartBracket, NodeType.EndBracket],
        [NodeType.Operand] = [NodeType.EndBracket, NodeType.BinaryOperator],
        [NodeType.Parameter] = [NodeType.EndBracket, NodeType.BinaryOperator],
        [NodeType.UnaryOperator] = [NodeType.UnaryOperator, NodeType.StartBracket, NodeType.Operand, NodeType.Parameter],
        [NodeType.BinaryOperator] = [NodeType.UnaryOperator, NodeType.StartBracket, NodeType.Operand, NodeType.Parameter],
        [NodeType.StartBracket] = [NodeType.UnaryOperator, NodeType.StartBracket, NodeType.Operand, NodeType.Parameter],
        [NodeType.EndBracket] = [NodeType.EndBracket, NodeType.BinaryOperator],
    };

    public Node[] GetNodes(string exp)
    {
        InitializeIfNot();

        var nodes = GetInitialNodes(exp);
        GrammarAnalysis(exp, nodes);
        return nodes;
    }

    public Node[] GetInitialNodes(string exp)
    {
        InitializeIfNot();

        if (exp.IsNullOrWhiteSpace()) exp = DefaultExpression;

        var match = ResolveRegex!.Match(exp);
        if (match.Success)
        {
            var nodes = match.Groups.OfType<Group>().Skip(1).First().Captures.OfType<Capture>().Select(capture =>
            {
                var value = capture.Value.Trim();
                var matchNode = PendingNodes!.FirstOrDefault(x => x.Value == value);

                if (matchNode is null)
                {
                    if (value.IsMatch(OperandRegex!))
                    {
                        return new Node(NodeType.Operand, value, capture.Index);
                    }
                    else if (value.IsMatch(ParameterRegex!))
                    {
                        return new Node(NodeType.Parameter, value, capture.Index);
                    }
                    else throw new NotImplementedException();
                }
                else
                {
                    NodeType nodeType = NodeType.Unspecified;

                    if (StartBrackets!.Contains(value)) nodeType |= NodeType.StartBracket;
                    if (EndBrackets!.Contains(value)) nodeType |= NodeType.EndBracket;
                    if (UnaryOperators!.Contains(value)) nodeType |= NodeType.UnaryOperator;
                    if (BinaryOperators!.Contains(value)) nodeType |= NodeType.BinaryOperator;

                    if (nodeType != NodeType.Unspecified)
                    {
                        return new Node(nodeType, value, capture.Index);
                    }
                    else throw new NotImplementedException();
                }
            }).ToArray();

            return nodes;
        }
        else throw new ArgumentException($"Invalid expression string({exp}).");
    }

    protected void GrammarAnalysis(string exp, Node[] nodes)
    {
        NodeType lastNodeType = NodeType.Unspecified;
        foreach (var node in nodes)
        {
            var nodeType = node.NodeType;
            if (lastNodeType == NodeType.Unspecified)
            {
                node.NodeType = nodeType.GetFlags().FirstOrDefault(x => FollowTypes[lastNodeType].Contains(x));
            }
            else
            {
                var followTypes = nodeType.GetFlags().SingleOrDefault(x => FollowTypes[lastNodeType].Contains(x));
                if (followTypes == NodeType.Unspecified) throw new ArgumentException(GetDebugString($"{nodeType} can not be located after {lastNodeType}. (Index: {node.Index})", exp, node));
                node.NodeType = followTypes;
            }

            lastNodeType = node.NodeType;
        }
    }

    protected static string GetDebugString(string prompt, string exp, Node node) => $@"{prompt}{Environment.NewLine}{exp}{Environment.NewLine}{" ".Repeat(node.Index)}↑";
    protected double Bracket(string start, string end, double operand)
    {
        return BracketFunctions[new(start, end)]!(operand);
    }

    public Expression GetExpression(string exp, out ParameterExpression dictionary)
    {
        InitializeIfNot();

        dictionary = Expression.Parameter(typeof(IDictionary<,>).MakeGenericType(typeof(string), typeof(double)), "p");
        return InnerBuild(exp, dictionary, typeof(IDictionary<string, double>));
    }

    protected Expression InnerBuild(string exp, Expression? parameter, Type? parameterType)
    {
        var nodes = GetNodes(exp);

        if (nodes.Any(x => x.NodeType == NodeType.Parameter) && (parameter is null || parameterType is null))
        {
            var node = nodes.First(x => x.NodeType == NodeType.Parameter);
            throw new ArgumentException(GetDebugString("Parameters are undefined. (Index: {node.Index})", exp, node));
        }

        var stack = new Stack<NodeExpressionPair>();
        var levelStack = new Stack<int>();

        void HandleUnary()
        {
            for (var peekPrev = stack.Skip(1).FirstOrDefault(); stack.Count > 1 && peekPrev.Node.NodeType == NodeType.UnaryOperator; peekPrev = stack.Skip(1).FirstOrDefault())
            {
                var current = stack.Pop();
                var prev = stack.Pop();
                var result = UnaryOpFunctions[prev.Node.Value]?.Invoke(current.Expression) ?? current.Expression;
                stack.Push(new NodeExpressionPair { Expression = result });
            }
        }

        void HandleBinary(int followLevel)
        {
            while (levelStack.Count > 0 && followLevel >= levelStack.Peek())
            {
                var peekPrev = stack.Skip(1).First();
                if (peekPrev.Node.NodeType == NodeType.BinaryOperator)
                {
                    var right = stack.Pop();
                    var op = stack.Pop();
                    var left = stack.Pop();
                    levelStack.Pop();

                    stack.Push(new NodeExpressionPair
                    {
                        Expression = BinaryOpFunctions[op.Node.Value](left.Expression, right.Expression),
                    });
                }
                else return;
            }
        }

        void HandleClose(Node node)
        {
            var endBracketValue = node.Value;
            var startBracketValues = StartBracketMap![endBracketValue];

            if (stack.Count >= 2)
            {
                var peekPrev = stack.Skip(1).FirstOrDefault();
                while (peekPrev.Node.NodeType != NodeType.StartBracket)
                {
                    HandleBinary(levelStack.Peek());
                    if (stack.Count >= 2) peekPrev = stack.Skip(1).FirstOrDefault();
                    else throw new ArgumentException(GetDebugString($"Unopend bracket. (Index: {node.Index})", exp, node));
                }

                var startBracketValue = peekPrev.Node.Value;
                if (startBracketValues.Contains(startBracketValue))
                {
                    var operand = stack.Pop();
                    stack.Pop();

                    var func = BracketFunctions[new(startBracketValue, endBracketValue)];
                    if (func is null) stack.Push(operand);
                    else stack.Push(new NodeExpressionPair
                    {
                        Expression = Expression.Call(Expression.Constant(this), BracketMethod,
                        [
                            Expression.Constant(startBracketValue),
                            Expression.Constant(endBracketValue),
                            operand.Expression ,
                        ]),
                    });

                    if (stack.Count >= 2)
                    {
                        peekPrev = stack.Skip(1).FirstOrDefault();
                        if (peekPrev.Node.NodeType == NodeType.UnaryOperator) HandleUnary();
                    }
                }
                else throw new ArgumentException(GetDebugString($"The start breacket ({peekPrev.Node.Value}) can not close the end bracket. (Index: {node.Index})", exp, node));
            }
            else throw new ArgumentException(GetDebugString($"Unopend bracket. (Index: {node.Index})", exp, node));
        }

        void HandleFinally()
        {
            if (stack.Any(x => x.Node?.NodeType == NodeType.StartBracket))
            {
                var pair = stack.Last(x => x.Node?.NodeType == NodeType.StartBracket);
                throw new ArgumentException(GetDebugString($"Unclosed bracket. (Index: {pair.Node.Index})", exp, pair.Node));
            }
            else if (stack.Any(x => x.Node?.NodeType == NodeType.UnaryOperator))
            {
                var pair = stack.First(x => x.Node?.NodeType == NodeType.UnaryOperator);
                throw new ArgumentException(GetDebugString($"Unary operator require an operand or parameter. (Index: {pair.Node.Index})", exp, pair.Node));
            }

            while (stack.Count > 1)
            {
                HandleBinary(int.MaxValue);
            }
        }

        foreach (var node in nodes)
        {
            var nodeType = node.NodeType;
            if (nodeType == NodeType.Parameter || nodeType == NodeType.Operand)
            {
                if (nodeType == NodeType.Operand)
                {
                    stack.Push(new NodeExpressionPair
                    {
                        Node = node,
                        Expression = OperandToExpression(node.Value),
                    });
                }
                else if (nodeType == NodeType.Parameter)
                {
                    stack.Push(new NodeExpressionPair
                    {
                        Node = node,
                        Expression = ParameterToExpression(GetParameterName(node.Value), parameter, parameterType),
                    });
                }

                if (stack.Count >= 2)
                {
                    var peekPrev = stack.Skip(1).First();
                    if (peekPrev.Node.NodeType == NodeType.UnaryOperator) HandleUnary();
                }
            }
            else if (nodeType == NodeType.BinaryOperator)
            {
                var level = BinaryOpLevels[node.Value];
                if (levelStack.Count > 0)
                {
                    var lastLevel = levelStack.Peek();
                    if (level >= lastLevel) HandleBinary(level);
                }

                levelStack.Push(level);
                stack.Push(new NodeExpressionPair { Node = node });
            }
            else if (nodeType == NodeType.UnaryOperator || nodeType == NodeType.StartBracket)
            {
                stack.Push(new NodeExpressionPair { Node = node });
            }
            else if (nodeType == NodeType.EndBracket) HandleClose(node);
            else throw new NotImplementedException($"Can not handle {nodeType}.");
        }

        HandleFinally();
        return stack.Pop().Expression;
    }

    public double Eval(string exp)
    {
        InitializeIfNot();

        var expression = InnerBuild(exp, null, null);
        var lambda = Expression.Lambda<Func<double>>(expression);
        var func = lambda.Compile();
        return func();
    }

    private static readonly MethodInfo ObjectToDictionaryMethod = ((Func<object, Dictionary<string, double>>)ObjectToDictionary).Method;
    private static Dictionary<string, double> ObjectToDictionary(object obj)
    {
        var props = obj.GetType().GetProperties();
        var dict = new Dictionary<string, double>();
        foreach (var prop in props)
        {
            var value = prop.GetValue(obj);
            dict[prop.Name] = (double)Convert.ChangeType(value, typeof(double))!;
        }
        return dict;
    }

    public Func<TParameter, double> Compile<TParameter>(string exp)
    {
        InitializeIfNot();

        var parameter = Expression.Parameter(typeof(TParameter), "p");
        var expression = InnerBuild(exp, parameter, typeof(TParameter));
        var lambda = Expression.Lambda<Func<TParameter, double>>(expression, parameter);
        return lambda.Compile();
    }

    public Func<object, double> Compile(string exp)
    {
        InitializeIfNot();

        var parameter = Expression.Parameter(typeof(object), "p");
        var typeIsIDictionaryExp = Expression.TypeIs(parameter, typeof(IDictionary<string, double>));

        var dictParameter =
            Expression.Condition(typeIsIDictionaryExp,
                Expression.Convert(parameter, typeof(IDictionary<string, double>)),
                Expression.Convert(Expression.Call(null, ObjectToDictionaryMethod, parameter), typeof(IDictionary<string, double>))
            );
        var expression = InnerBuild(exp, dictParameter, typeof(IDictionary<string, double>));
        var lambda = Expression.Lambda<Func<object, double>>(expression, parameter);
        return lambda.Compile();
    }

    protected void ThrowIf_DefineAfterInitializing()
    {
        if (Initialized) throw new InvalidOperationException("Define operations should be used in the constructor.");
    }

    public void DefineBracket(Bracket key, UnaryFunc<double> value)
    {
        ThrowIf_DefineAfterInitializing();
        BracketFunctions.Add(key, value);
    }
    public void Define(string key, UnaryFunc<double> value)
    {
        ThrowIf_DefineAfterInitializing();
        UnaryOpFunctions.Add(key, exp => Expression.Call(Expression.Constant(value.Target), value.Method, exp));
    }
    public void Define(string key, int priority, BinaryFunc<double> value)
    {
        ThrowIf_DefineAfterInitializing();
        BinaryOpFunctions.Add(key, (left, rigth) => Expression.Call(Expression.Constant(value.Target), value.Method, left, rigth));
        BinaryOpLevels.Add(key, priority);
    }

    protected struct NodeExpressionPair
    {
        public Node Node { get; set; }
        public Expression Expression { get; set; }
    }

}
