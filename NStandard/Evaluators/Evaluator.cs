﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace NStandard.Evaluators
{
    public static class Evaluator
    {
        public static readonly NumericalEvaluator Numerical = new();
    }

    public abstract class EvaluatorBase
    {
        private static readonly MethodInfo BracketMethod = typeof(EvaluatorBase).GetDeclaredMethod(nameof(Bracket));
        private static readonly MethodInfo DictionaryGetItemMethod = typeof(IDictionary<,>).MakeGenericType(typeof(string), typeof(double)).GetMethod("get_Item");
        private static readonly MethodInfo DictionaryContainsKeyMethod = typeof(IDictionary<,>).MakeGenericType(typeof(string), typeof(double)).GetMethod("ContainsKey");
        private static readonly Regex NormalRegex = new(@"([\[\]\-\.\^\$\{\}\?\+\*\|\(\)])");

        protected abstract string DefaultExpression { get; }
        protected abstract string OperandRegexString { get; }
        protected abstract Expression OperandToExpression(string operand);
        protected abstract string ParameterRegexString { get; }
        protected abstract Func<string, string> GetParameterName { get; }

        protected abstract Dictionary<string, int> BinaryOperatorLevels { get; }
        protected abstract Dictionary<string, UnaryOpFunc<Expression>> UnaryOpFunctions { get; }
        protected abstract Dictionary<string, BinaryOpFunc<Expression>> BinaryOpFunctions { get; }
#if NET35 || NET40 || NET45 || NET451 || NET452 || NET46
        protected abstract Dictionary<Tuple<string, string>, UnaryOpFunc<double>> BracketFunctions { get; }
#else
        protected abstract Dictionary<(string, string), UnaryOpFunc<double>> BracketFunctions { get; }
#endif

        protected string[] UnaryOperators { get; private set; }
        protected string[] BinaryOperators { get; private set; }
        protected string[] StartBrackets { get; private set; }
        protected string[] EndBrackets { get; private set; }
        protected Dictionary<string, string[]> StartBracketMap { get; private set; }
        protected IEnumerable<Node> PendingNodes { get; private set; }

        protected Regex OperandRegex { get; private set; }
        protected Regex ParameterRegex { get; private set; }
        protected Regex ResolveRegex { get; private set; }

        public EvaluatorBase()
        {
            var undefinedOps = BinaryOperatorLevels.Keys.Where(x => !BinaryOpFunctions.Keys.Contains(x));
            if (undefinedOps.Any()) throw new ArgumentException($"Some operators are undefined. ({undefinedOps.Join(",")})");

            var undefinedLevels = BinaryOpFunctions.Keys.Where(x => !BinaryOperatorLevels.Keys.Contains(x));
            if (undefinedLevels.Any()) throw new ArgumentException($"Some operators are undefined. ({undefinedLevels.Join(",")})");

            Initialize();
        }

        public void Initialize()
        {
            UnaryOperators = UnaryOpFunctions.Keys.ToArray();
            BinaryOperators = BinaryOpFunctions.Keys.ToArray();
            StartBrackets = BracketFunctions.Select(x => x.Key.Item1).ToArray();
            EndBrackets = BracketFunctions.Select(x => x.Key.Item2).Distinct().ToArray();
            StartBracketMap = BracketFunctions
                .Select(x => x.Key).GroupBy(x => x.Item2)
                .ToDictionary(x => x.Key, x => x.Select(i => i.Item1).ToArray());

            OperandRegex = new Regex(OperandRegexString);
            ParameterRegex = new Regex(ParameterRegexString);

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
                foreach (var value in UnaryOperators) yield return new Node { NodeType = NodeType.UnaryOperator, Value = value };
                foreach (var value in BinaryOperators) yield return new Node { NodeType = NodeType.BinaryOperator, Value = value };
                foreach (var value in StartBrackets) yield return new Node { NodeType = NodeType.StartBracket, Value = value };
                foreach (var value in EndBrackets) yield return new Node { NodeType = NodeType.EndBracket, Value = value };
            }
            PendingNodes = GetPendingNodes().OrderByDescending(x => x.Value.Length);
        }

        protected string NormalRegexString(string origin) => origin.RegexReplace(NormalRegex, "\\$1");

        protected Expression ParameterToExpression(string name, ParameterExpression parameter, Type parameterType)
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
                                Expression.Equal(Expression.Convert(propertyOrField, typeof(bool)), Expression.Constant((bool?)true)),
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
            [NodeType.Operand] = new[] { NodeType.EndBracket, NodeType.BinaryOperator },
            [NodeType.Parameter] = new[] { NodeType.EndBracket, NodeType.BinaryOperator },
            [NodeType.UnaryOperator] = new[] { NodeType.UnaryOperator, NodeType.StartBracket, NodeType.Operand, NodeType.Parameter },
            [NodeType.BinaryOperator] = new[] { NodeType.UnaryOperator, NodeType.StartBracket, NodeType.Operand, NodeType.Parameter },
            [NodeType.StartBracket] = new[] { NodeType.UnaryOperator, NodeType.StartBracket, NodeType.Operand, NodeType.Parameter },
            [NodeType.EndBracket] = new[] { NodeType.EndBracket, NodeType.BinaryOperator },
        };

        public Node[] GetNodes(string exp)
        {
            if (exp.IsNullOrWhiteSpace()) exp = DefaultExpression;

            var match = ResolveRegex.Match(exp);
            if (match.Success)
            {
                var nodes = match.Groups.OfType<Group>().Skip(1).First().Captures.OfType<Capture>().Select(capture =>
                {
                    var value = capture.Value.Trim();

                    var matchNode = PendingNodes.FirstOrDefault(x => x.Value == value);
                    if (matchNode is null)
                    {
                        if (value.IsMatch(OperandRegex))
                        {
                            return new Node
                            {
                                NodeType = NodeType.Operand,
                                Index = capture.Index,
                                Value = value,
                            };
                        }
                        else if (value.IsMatch(ParameterRegex))
                        {
                            return new Node
                            {
                                NodeType = NodeType.Parameter,
                                Index = capture.Index,
                                Value = value,
                            };
                        }
                        else throw new NotImplementedException();
                    }
                    else if (UnaryOperators.Contains(value))
                    {
                        return new Node
                        {
                            NodeType = NodeType.UnaryOperator,
                            Index = capture.Index,
                            Value = value,
                        };
                    }
                    else if (BinaryOperators.Contains(value))
                    {
                        return new Node
                        {
                            NodeType = NodeType.BinaryOperator,
                            Index = capture.Index,
                            Value = value,
                        };
                    }
                    else if (StartBrackets.Contains(value))
                    {
                        return new Node
                        {
                            NodeType = NodeType.StartBracket,
                            Index = capture.Index,
                            Value = value,
                        };
                    }
                    else if (EndBrackets.Contains(value))
                    {
                        return new Node
                        {
                            NodeType = NodeType.EndBracket,
                            Index = capture.Index,
                            Value = value,
                        };
                    }
                    else throw new NotImplementedException();
                }).ToArray();

                return nodes;
            }
            else throw new ArgumentException($"Invalid expression string({exp}).");
        }

        protected string GetDebugString(string prompt, string exp, Node node) => $@"{prompt}{Environment.NewLine}{exp}{Environment.NewLine}{" ".Repeat(node.Index)}↑";
        protected double Bracket(string start, string end, double operand)
        {
#if NET35 || NET40 || NET45 || NET451 || NET452 || NET46
            return BracketFunctions[Tuple.Create(start, end)](operand);
#else
            return BracketFunctions[(start, end)](operand);
#endif
        }

        protected Expression InnerBuild(string exp, ParameterExpression parameter, Type parameterType)
        {
            var nodes = GetNodes(exp);

            if (nodes.Any(x => x.NodeType == NodeType.Parameter) && (parameter is null || parameterType is null))
            {
                var node = nodes.First(x => x.NodeType == NodeType.Parameter);
                throw new ArgumentException(GetDebugString("Parameters are undefined.", exp, node));
            }

            var stack = new Stack<NodeExpressionPair>();
            var levelStack = new Stack<int>();

            void HandleUnary()
            {
                for (var peekPrev = stack.Skip(1).FirstOrDefault(); stack.Count > 1 && peekPrev.Node.NodeType == NodeType.UnaryOperator; peekPrev = stack.Skip(1).FirstOrDefault())
                {
                    var current = stack.Pop();
                    var prev = stack.Pop();
                    var result = UnaryOpFunctions[prev.Node.Value](current.Expression);
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
                var startBracketValues = StartBracketMap[endBracketValue];

                if (stack.Count >= 2)
                {
                    var peekPrev = stack.Skip(1).FirstOrDefault();
                    while (peekPrev.Node.NodeType != NodeType.StartBracket)
                    {
                        HandleBinary(levelStack.Peek());
                        if (stack.Count >= 2) peekPrev = stack.Skip(1).FirstOrDefault();
                        else throw new ArgumentException(GetDebugString($"Unopend bracket.", exp, node));
                    }

                    var startBracketValue = peekPrev.Node.Value;
                    if (startBracketValues.Contains(startBracketValue))
                    {
                        var operand = stack.Pop();
                        stack.Pop();

#if NET35 || NET40 || NET45 || NET451 || NET452 || NET46
                        var func = BracketFunctions[Tuple.Create(startBracketValue, endBracketValue)];
#else
                        var func = BracketFunctions[(startBracketValue, endBracketValue)];
#endif
                        if (func is null) stack.Push(operand);
                        else stack.Push(new NodeExpressionPair
                        {
                            Expression = Expression.Call(Expression.Constant(this), BracketMethod, new[]
                            {
                                Expression.Constant(startBracketValue),
                                Expression.Constant(endBracketValue),
                                operand.Expression ,
                            }),
                        });

                        if (stack.Count >= 2)
                        {
                            peekPrev = stack.Skip(1).FirstOrDefault();
                            if (peekPrev.Node.NodeType == NodeType.UnaryOperator) HandleUnary();
                        }
                    }
                    else throw new ArgumentException(GetDebugString($"The start breacket ({peekPrev.Node.Value}) can not close the end bracket.", exp, node));
                }
                else throw new ArgumentException(GetDebugString($"Unopend bracket.", exp, node));
            }

            void HandleFinally()
            {
                if (stack.Any(x => x.Node?.NodeType == NodeType.StartBracket))
                {
                    var pair = stack.Last(x => x.Node?.NodeType == NodeType.StartBracket);
                    throw new ArgumentException(GetDebugString($"Unclosed bracket.", exp, pair.Node));
                }
                else if (stack.Any(x => x.Node?.NodeType == NodeType.UnaryOperator))
                {
                    var pair = stack.First(x => x.Node?.NodeType == NodeType.UnaryOperator);
                    throw new ArgumentException(GetDebugString($"Unary operator require an operand or parameter.", exp, pair.Node));
                }

                while (stack.Count > 1)
                {
                    HandleBinary(int.MaxValue);
                }
            }

            NodeType lastNodeType = NodeType.Unspecified;
            foreach (var node in nodes)
            {
                var nodeType = node.NodeType;
                if (lastNodeType != NodeType.Unspecified && !FollowTypes[lastNodeType].Contains(nodeType)) throw new ArgumentException(GetDebugString($"{nodeType} can not come after {lastNodeType}.", exp, node));
                lastNodeType = nodeType;

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
                    var level = BinaryOperatorLevels[node.Value];
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

        public double Eval(string exp) => Compile(exp)();
        public double Eval(string exp, IDictionary<string, double> dict) => CompileParameterized(exp)(dict);
        public double Eval<TParameter>(string exp, TParameter parameter) => CompileParameterized<TParameter>(exp)(parameter);

        public Expression Build(string exp) => InnerBuild(exp, null, null);
        public Expression BuildParameterized(string exp, out ParameterExpression dictionary)
        {
            dictionary = Expression.Parameter(typeof(IDictionary<,>).MakeGenericType(typeof(string), typeof(double)), "p");
            return InnerBuild(exp, dictionary, typeof(IDictionary<string, double>));
        }
        public Expression BuildParameterized<TParameter>(string exp, out ParameterExpression parameter)
        {
            parameter = Expression.Parameter(typeof(TParameter), "p");
            return InnerBuild(exp, parameter, typeof(TParameter));
        }

        public Func<double> Compile(string exp) => Expression.Lambda<Func<double>>(Build(exp)).Compile();
        public Func<IDictionary<string, double>, double> CompileParameterized(string exp)
        {
            var expression = BuildParameterized(exp, out var dictionary);
            return Expression.Lambda<Func<IDictionary<string, double>, double>>(expression, dictionary).Compile();
        }

        public Func<TParameter, double> CompileParameterized<TParameter>(string exp)
        {
            var expression = BuildParameterized<TParameter>(exp, out var parameter);
            return Expression.Lambda<Func<TParameter, double>>(expression, parameter).Compile();
        }

        protected struct NodeExpressionPair
        {
            public Node Node { get; set; }
            public Expression Expression { get; set; }
        }

    }
}
