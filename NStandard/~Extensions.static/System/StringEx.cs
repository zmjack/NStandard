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
        public static string[] SplitIntoLines(string source, int lineLength)
        {
            var ret = source.AsIndexValuePairs()
                .GroupBy(x => x.Index / lineLength)
                .Select(g => new string(g.Select(x => x.Value).ToArray()))
                .ToArray();
            return ret;
        }

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
        /// Converts a string to PascalCase string. e.g. DotNET.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string PascalCase(string source)
        {
            if (source.IsNullOrEmpty()) return source;

            static bool IsNotUpperAndNotUnderCross(char c) => c is < 'A' or > 'Z' && c != '_';
            var index = source.IndexOf(IsNotUpperAndNotUnderCross);

            return index switch
            {
                int when index == 0 => $"{char.ToUpper(source[0])}{source.Substring(1)}",
                _ => source,
            };
        }

        /// <summary>
        /// Converts a string to CamelCase string. e.g. dotNET.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string CamelCase(string source)
        {
            if (source.IsNullOrEmpty()) return source;

            static bool IsNotUpper(char c) => c is < 'A' or > 'Z';
            var index = source.IndexOf(IsNotUpper);

            return index switch
            {
                int when index > 1 => $"{source.Slice(0, index - 1).ToLower()}{source[index - 1]}{source.Substring(index)}",
                int when index == 1 => $"{char.ToLower(source[0])}{source.Substring(index)}",
                int when index == 0 => source,
                _ => source.ToLower(),
            };
        }

        /// <summary>
        /// Converts a string to KebabCase string. e.g. dot-net.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string KebabCase(string source)
        {
            if (source.IsNullOrEmpty()) return source;

            source = source.Replace("_", "-");

            static bool IsUpper(char c) => c is >= 'A' and <= 'Z';
            static bool IsNotUpper(char c) => c is < 'A' or > 'Z';
            static bool IsHyphen(char c) => c == '-';

            static bool IsUpperOrHyphen(char c) => c is >= 'A' and <= 'Z' || c == '-';
            static bool IsNotUpperOrHyphen(char c) => c is < 'A' or > 'Z' || c == '-';

            var sb = new StringBuilder();
            string GetPattern(int _startIndex)
            {
                switch (_startIndex)
                {
                    case int when _startIndex == source.Length - 1: return "$";

                    case int when _startIndex < source.Length - 1:
                        var c1 = source[_startIndex];
                        if (IsHyphen(c1)) return "-";

                        var c2 = source[_startIndex + 1];
                        if (IsUpper(c1))
                        {
                            if (IsHyphen(c2)) return "A-";
                            else if (IsUpper(c2)) return "AA";
                            else return "Ab";
                        }
                        else if (IsNotUpper(c1))
                        {
                            if (IsHyphen(c2)) return "b-";
                            if (IsUpper(c2)) return "bA";
                            else return "bb";
                        }
                        else throw new NotImplementedException();

                    default: throw new NotImplementedException();
                }
            }

            var startIndex = 0;
            while (startIndex != -1)
            {
                var pattern = GetPattern(startIndex);
                int index;
                var prefix = startIndex == 0 ? "" : "-";

                if (pattern == "$")
                {
                    sb.Append($"{prefix}{source[startIndex].ToLower()}");
                    break;
                }
                else if (pattern == "-")
                {
                    startIndex++;
                }
                else if (pattern == "A-")
                {
                    sb.Append($"{source[startIndex].ToLower()}-");
                    startIndex += 2;
                }
                else if (pattern == "b-")
                {
                    sb.Append($"{source[startIndex]}-");
                    startIndex += 2;
                }
                else if (pattern == "Ab" || pattern == "bb")
                {
                    index = source.IndexOf(IsUpperOrHyphen, startIndex + 1);

                    if (index != -1)
                    {
                        sb.Append($"{prefix}{source.Slice(startIndex, index).ToLower()}");
                        startIndex = index;
                    }
                    else
                    {
                        sb.Append($"{prefix}{source.Slice(startIndex).ToLower()}");
                        break;
                    }
                }
                else if (pattern == "bA")
                {
                    sb.Append($"{source[startIndex]}");
                    startIndex++;
                }
                else if (pattern == "AA")
                {
                    index = source.IndexOf(IsNotUpperOrHyphen, startIndex + 1);

                    if (index != -1)
                    {
                        if (!IsHyphen(source[index])) index--;

                        sb.Append($"{prefix}{source.Slice(startIndex, index).ToLower()}");
                        startIndex = index;
                    }
                    else
                    {
                        sb.Append($"{prefix}{source.Slice(startIndex).ToLower()}");
                        break;
                    }
                }
            }

            return sb.ToString();
        }

        public static bool IsValidFileName(string source, PlatformID? platformID = null)
        {
            platformID ??= Environment.OSVersion.Platform;

            switch (platformID)
            {
                case PlatformID.Win32NT:
                    var specialNames = new[]
                    {
                        "CON", "PRN", "AUX", "NUL",
                        "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
                        "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9",
                    };

                    if (source.Any(c => c < 32 || new[] { '\\', '/', ':', '*', '?', '"', '<', '>', '|' }.Contains(c))
                        || specialNames.Contains(source)
                        || source.EndsWith(".")
                        || source.EndsWith(" "))
                        return false;
                    else return true;

                case PlatformID.Unix: return !source.Any(c => new[] { '\0', '/' }.Contains(c));
                default: throw new NotSupportedException("Only these platforms are supported: Win32NT, Unix.");
            }
        }

        /// <summary>
        /// Projects some strings back into an instance's field or property. (Using `?` on the right side of a variable disables greedy matching)
        /// </summary>
        /// <typeparam name="TClass"></typeparam>
        /// <param name="source"></param>
        /// <param name="instance"></param>
        /// <param name="patternExp"></param>
        public static void Extract<TClass>(string source, TClass instance, Expression<Func<TClass, FormattableString>> patternExp) where TClass : class
        {
            var arguments = (patternExp.Body as MethodCallExpression)?.Arguments;
            if (arguments is null)
                throw new ArgumentException($"The argument `{nameof(patternExp)}` must be return a single line FormattableString.");

            var format = (arguments[0] as ConstantExpression).Value.ToString();
            var members = (arguments[1] as NewArrayExpression).Expressions.Select(exp => exp switch
            {
                MemberExpression memberExp => memberExp.Member,
                UnaryExpression unaryExp => (unaryExp.Operand as MemberExpression).Member,
                _ => throw new ArgumentException("At least one member info can not be accessed."),
            }).ToArray();

            var prePattern = new[] { "/", @"\", "+", "*", "[", "]", "(", ")", "?", "|", "^" }
                .Aggregate(format, (_acc, ch) => _acc.Replace(ch, $"\\{ch}"));
            var contentPattern = RangeEx.Create(0, members.Length)
                .Aggregate(prePattern, (acc, i) => acc.Replace($"{{{i}}}\\?", @"(.*?)").Replace($"{{{i}}}", @"(.*)"));

            var pattern = $"^{contentPattern}$";
            var regex = new Regex(pattern, RegexOptions.Singleline);

            var match = regex.Match(source);
            if (match.Success)
            {
                foreach (var i in new int[members.Length].Let(i => i + 1))
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
    }
}
