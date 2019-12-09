using NStandard;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace NStandard
{
    public static class StringEx
    {
        /// <summary>
        /// Get the common starts of the specified strings.
        /// </summary>
        /// <param name="strings"></param>
        /// <returns></returns>
        public static string CommonStarts(params string[] strings)
        {
            if (strings.Length == 0) return string.Empty;
            else if (strings.Length == 1) return strings[0];
            else
            {
                var minLength = strings.Min(x => x.Length);
                var sb = new StringBuilder(minLength);

                for (int i = 0; i < minLength; i++)
                {
                    var take = strings[0][i];
                    if (strings.All(x => x[i] == take))
                        sb.Append(take);
                    else break;
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Converts a string to CamelCase string.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string CamelCase(string source)
        {
            if (source.IsNullOrEmpty()) return source;
            if (!source.All(c => 31 < c && c < 127)) throw new ArgumentException("Some chars is out of allowed range(ascii, 32 to 126).");

            bool IsNotUpper(char c) => c < 'A' || 'Z' < c;
            var index = source.IndexOf(IsNotUpper);

            switch (index)
            {
                case int _ when index > 1: return $"{source.Slice(0, index - 1).ToLower()}{source[index - 1]}{source.Substring(index)}";
                case int _ when index == 1: return $"{char.ToLower(source[0])}{source.Substring(index)}";
                case int _ when index == 0: return source;
                default: return source.ToLower();
            }
        }

        /// <summary>
        /// Converts a string to KebabCase string.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string KebabCase(string source)
        {
            if (source.IsNullOrEmpty()) return source;
            if (!source.All(c => 31 < c && c < 127)) throw new ArgumentException("Some chars is out of allowed range(ascii, 32 to 126).");

            bool IsUpper(char c) => 'A' <= c && c <= 'Z';
            bool IsNotUpper(char c) => c < 'A' || 'Z' < c;

            var sb = new StringBuilder();
            string GetPattern(int _startIndex)
            {
                switch (_startIndex)
                {
                    case int _ when _startIndex == source.Length - 1:
                        return "x";

                    case int _ when _startIndex < source.Length - 1:
                        var c1 = source[_startIndex];
                        var c2 = source[_startIndex + 1];

                        if (IsUpper(c1) && IsUpper(c2)) return "AA";
                        else if (IsUpper(c1) && IsNotUpper(c2)) return "Ab";
                        else if (IsNotUpper(c1) && IsUpper(c2)) return "bA";
                        else if (IsNotUpper(c1) && IsNotUpper(c2)) return "bb";
                        else goto default;

                    default: return null;
                }
            }

            var startIndex = 0;
            while (startIndex != -1)
            {
                var pattern = GetPattern(startIndex);
                int index;
                switch (pattern)
                {
                    case "x":
                        sb.Append(source[startIndex]);
                        startIndex++;
                        break;

                    case "bb":
                    case "Ab":
                        index = source.IndexOf(IsUpper, startIndex + 1);
                        if (index != -1)
                            sb.Append($"-{source.Slice(startIndex, index).ToLower()}");
                        else sb.Append($"-{source.Slice(startIndex).ToLower()}");
                        startIndex = index;
                        break;

                    case "bA":
                        sb.Append($"-{source[startIndex]}");
                        break;

                    case "AA":
                        index = source.IndexOf(IsNotUpper, startIndex + 1);
                        if (index != -1)
                        {
                            sb.Append($"-{source.Slice(startIndex, index - 1).ToLower()}");
                            startIndex = index - 1;
                        }
                        else
                        {
                            sb.Append($"-{source.Slice(startIndex).ToLower()}");
                            startIndex = -1;
                        }
                        break;

                    default: throw new InvalidOperationException();
                }
            }

            return sb.ToString().Substring(1);
        }

#if NETSTANDARD2_0
        /// <summary>
        /// Projects some strings back into an instance's field or property. (Using `?` on the right side of a variable disables greedy matching)
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="source"></param>
        /// <param name="instance"></param>
        /// <param name="patternExp"></param>
        public static void Extract<TClass>(string source, TClass instance, Expression<Func<TClass, FormattableString>> patternExp)
            where TClass : class
        {
            var arguments = (patternExp.Body as MethodCallExpression)?.Arguments;
            if (arguments is null)
                throw new ArgumentException($"The argument `{nameof(patternExp)}` must be return a single line FormattableString.");

            var format = (arguments[0] as ConstantExpression).Value.ToString();
            var members = (arguments[1] as NewArrayExpression).Expressions.Select(exp =>
            {
                switch (exp)
                {
                    case MemberExpression memberExp: return memberExp.Member;
                    case UnaryExpression unaryExp: return (unaryExp.Operand as MemberExpression).Member;
                    default: throw new ArgumentException("At least one member info can not be accessed.");
                }
            }).ToArray();

            var prePattern = new[] { "/", "+", "*", "[", "]", "(", ")", "?", "|", "^" }
                .Aggregate(format, (_acc, ch) => _acc.Replace(ch, $"\\{ch}"));
            var pattern = new int[members.Length].Let(i => i)                   // IntegerRange(0, members.Length - 1)
                .Aggregate(prePattern, (acc, i) => acc.Replace($"{{{i}}}\\?", @"(.*?)").Replace($"{{{i}}}", @"(.*)"))
                .For(x => $"^{x}$");
            var regex = new Regex(pattern, RegexOptions.Singleline);

            var match = regex.Match(source);
            if (match.Success)
            {
                foreach (var i in new int[members.Length].Let(i => i + 1))      // IntegerRange(1, members.Length))
                {
                    var value = match.Groups[i].Value;
                    switch (members[i - 1])
                    {
                        case FieldInfo member:
                            member.SetValue(instance, Convert.ChangeType(value, member.FieldType));
                            break;

                        case PropertyInfo member:
                            member.SetValue(instance, Convert.ChangeType(value, member.PropertyType));
                            break;

                        default:
                            throw new ArgumentException($"The access member must be {nameof(FieldInfo)} or {nameof(PropertyInfo)}.");
                    }
                }
            }
            else throw new ArgumentException($"The argument `{nameof(patternExp)}` can not match the specified string.");
        }
#endif

    }
}
