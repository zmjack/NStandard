using NStandard.Algorithms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NStandard
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class XString
    {
        /// <summary>
        /// Indicates whether the specified string is null or an System.String.Empty string.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string @this) => string.IsNullOrEmpty(@this);

        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string @this) => string.IsNullOrWhiteSpace(@this);

        /// <summary>
        /// Indicates whether the specified string is an System.String.Empty string.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string @this) => @this.Length == 0;

        /// <summary>
        /// Indicates whether a specified string is empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool IsWhiteSpace(this string @this)
        {
            foreach (var c in @this)
                if (!char.IsWhiteSpace(c)) return false;
            return true;
        }

        /// <summary>
        /// Returns centered in a string of length width. Padding is done using the specified fillchar (default is an ASCII space).
        /// </summary>
        /// <param name="this"></param>
        /// <param name="widthA"></param>
        /// <param name="fillChar"></param>
        /// <returns></returns>
        public static string Center(this string @this, int widthA, char fillChar = ' ')
        {
            var len = @this.GetLengthA();
            if (widthA <= len) return @this;

            var total = widthA - len;
            var right = total / 2;
            var left = right;
            if (total.IsOdd()) left += 1;

            var sb = new StringBuilder(widthA);

            for (int i = 0; i < left; i++) sb.Append(fillChar);
            sb.Append(@this);
            for (int i = 0; i < right; i++) sb.Append(fillChar);

            return sb.ToString();
        }

        /// <summary>
        /// Indicates whether the string matches the specified regular expression.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static bool IsMatch(this string @this, Regex regex) => regex.Match(@this).Success;

        /// <summary>
        /// Retrieves a substring from this instance. The substring starts at a specified
        ///     character position and continues to the end of the string.
        ///     (If the parameter is negative, the search will start on the right.)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        public static string Slice(this string @this, int start) => Slice(@this, start, @this.Length);

        /// <summary>
        /// Retrieves a substring from this instance. The substring starts at a specified
        ///     character position and ends with a specified character position.
        ///     (If the parameters is negative, the search will start on the right.)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static string Slice(this string @this, int start, int stop)
        {
            start = GetCharPosition(ref @this, start);
            stop = GetCharPosition(ref @this, stop);

            var length = stop - start;
            return @this.Substring(start, length);
        }
        private static int GetCharPosition(ref string str, int pos) => pos < 0 ? str.Length + pos : pos;

        /// <summary>
        /// Returns the char at a specified index in the string.
        ///     (If the parameter is negative, the search will start on the right.)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static char CharAt(this string @this, int pos) => @this[GetCharPosition(ref @this, pos)];

        /// <summary>
        /// Returns a string which is equivalent to adding it to itself n times.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="times"></param>
        /// <returns></returns>
        public static string Repeat(this string @this, int times)
        {
            var sb = new StringBuilder(@this.Length * times);
            for (int i = 0; i < times; i++)
                sb.Append(@this);
            return sb.ToString();
        }

        /// <summary>
        /// Returns the number of occurrences of substring which in the specified string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="searchString"></param>
        /// <param name="overlapping"></param>
        /// <returns></returns>
        public static int Count(this string @this, string searchString, bool overlapping = false)
            => new Kmp(searchString).Count(@this, overlapping);

        /// <summary>
        /// Returns a new string which is normalized by the newline string of current environment.
        /// <para>If a string come from non-Unix platforms, then its NewLine is "\r\n".</para>
        /// <para>If a string come from Unix platforms, then its NewLine is "\n".</para>
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string NormalizeNewLine(this string @this)
        {
            switch (Environment.NewLine)
            {
                case "\r\n":
                    return new Regex("(?<!\r)\n", RegexOptions.Singleline).Replace(@this, "\r\n");
                case "\n":
                    return new Regex("\r\n", RegexOptions.Singleline).Replace(@this, "\n");

                default: return @this;
            }
        }

        /// <summary>
        /// Divides a string into multi-lines (ignore Empty or WhiteSpace). If the string is null, return string[0]. 
        /// (Perhaps you should set `normalizeNewLine` to true to convert the NewLine 
        ///     which is defined in other system into the current system's.)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="normalizeNewLine"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetPureLines(this string @this, bool normalizeNewLine = false)
            => GetLines(@this, normalizeNewLine, true);

        /// <summary>
        /// Divides a string into multi-lines. If the string is null, return string[0]. 
        /// (Perhaps you should set `normalizeNewLine` to true to convert the NewLine 
        ///     which is defined in other system into the current system's.)
        /// </summary>
        /// <param name="this"></param>
        /// <param name="normalizeNewLine"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetLines(this string @this, bool normalizeNewLine = false)
            => GetLines(@this, normalizeNewLine, false);

        private static IEnumerable<string> GetLines(this string @this, bool normalizeNewLine = false, bool ignoreEmptyOrWhiteSpace = false)
        {
            if (normalizeNewLine)
                @this = @this.NormalizeNewLine();

            if (@this != null)
            {
                var newLineLength = Environment.NewLine.Length;
                var startIndex = 0;
                var findIndex = -1;

                while ((findIndex = @this.IndexOf(Environment.NewLine, startIndex)) >= 0)
                {
                    var line = @this.Slice(startIndex, findIndex);
                    startIndex = findIndex + newLineLength;
                    if (!(ignoreEmptyOrWhiteSpace && string.IsNullOrWhiteSpace(line)))
                        yield return line;
                }

                if (startIndex != @this.Length)
                {
                    var line = @this.Slice(startIndex, @this.Length);
                    if (!(ignoreEmptyOrWhiteSpace && string.IsNullOrWhiteSpace(line)))
                        yield return line;
                }
            }
        }

        /// <summary>
        /// Removes all leading and trailing white-space characters from the current string,
        ///     and replaces multiple spaces with a single.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string Unique(this string @this)
            => new Regex(@"[\s]{2,}").Replace(@this.NormalizeNewLine().Replace(Environment.NewLine, " ").Trim(), " ");

        /// <summary>
        /// In a specified input string, replaces all strings that match a regular expression
        //      pattern with a specified replacement string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="regex"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string RegexReplace(this string @this, string regex, string replacement)
            => new Regex(regex, RegexOptions.Singleline).Replace(@this, replacement);

        /// <summary>
        /// In a specified input string, replaces all strings that match a regular expression
        //      pattern with a specified replacement string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="regex"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string RegexReplace(this string @this, Regex regex, string replacement)
            => regex.Replace(@this, replacement);

        /// <summary>
        /// In a specified input string, replaces all strings that match a regular expression
        //      pattern with a specified replacement string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="regex"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string RegexReplace(this string @this, Regex regex, MatchEvaluator evaluator)
            => regex.Replace(@this, evaluator);

        /// <summary>
        /// Projects the specified string to a new string by using regular expressions (using Single-line Mode).
        ///     If there is no match, this method returns null.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="regex"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        [Obsolete("Replace with the new function: string Project(this string @this, Regex regex, string target = null)", true)]
        public static string Project(this string @this, string regex, string target = null) => Project(@this, new Regex(regex, RegexOptions.Singleline), target);

        /// <summary>
        /// Projects the specified string to a new string by using regular expressions.
        ///     If there is no match, this method returns null.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="regex"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string Project(this string @this, Regex regex, string target = null)
        {
            var match = regex.Match(@this);
            if (match.Success)
            {
                if (target is null)
                    return string.Join("", match.Groups.OfType<Group>().Skip(1).Select(g => g.Value));
                else return regex.Replace(match.Groups[0].Value, target);
            }
            else return null;
        }

        /// <summary>
        /// Projects the specified string to a new string by using regular expressions (using Single-line Mode).
        ///     If there is no match, this method returns null.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        [Obsolete("Replace with the new function: string[][] ProjectToArray(this string @this, Regex regex)", true)]
        public static string[][] ProjectToArray(this string @this, string regex)
            => ProjectToArray(@this, new Regex(regex, RegexOptions.Singleline));

        /// <summary>
        /// Projects the specified string to a new string by using regular expressions.
        ///     If there is no match, this method returns null.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static string[][] ProjectToArray(this string @this, Regex regex)
        {
            var match = regex.Match(@this);
            if (match.Success)
                return match.Groups.OfType<Group>()
                    .Select(g => g.Captures.OfType<Capture>().Select(c => c.Value).ToArray()).ToArray();
            else return null;
        }

        /// <summary>
        /// Returns a new string which is consisting of many units by the specified length number of characters.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="separator"></param>
        /// <param name="unitLength"></param>
        /// <param name="padRight"></param>
        /// <returns></returns>
        public static string UnitInsert(this string @this, int unitLength, string separator = " ", bool padRight = false)
        {
            var sb = new StringBuilder(@this.Length + (int)Math.Ceiling((double)@this.Length / unitLength));
            int pos;

            if (!padRight)
                pos = (@this.Length % unitLength).For(_ => _ > 0 ? _ : unitLength);
            else pos = unitLength;

            foreach (var ch in @this)
            {
                if (pos == 0)
                {
                    sb.Append(separator);
                    pos = unitLength;
                }

                sb.Append(ch);
                pos--;
            }
            return sb.ToString();
        }

        /// <summary>
        /// Returns a copy of the string with its first character capitalized.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public static string CapitalizeFirst(this string @this, bool upper = true)
        {
            var chars = @this.ToCharArray();
            if (chars.Length > 0)
            {
                if (upper) chars[0] = char.ToUpper(chars[0]);
                else chars[0] = char.ToLower(chars[0]);
            }
            return new string(chars);
        }

        /// <summary>
        /// Gets the length of the specified string as Ascii.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int GetLengthA(this string @this)
            => @this.Aggregate(0, (acc, cur) => acc += cur.GetLengthA());

        /// <summary>
        /// Returns a new string that right-aligns the characters in this string by padding
        ///     them on the right with a specified Ascii character, for a specified total length.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="totalWidth"></param>
        /// <returns></returns>
        public static string PadLeftA(this string @this, int totalWidth) => PadLeftA(@this, totalWidth, ' ');
        /// <summary>
        /// Returns a new string that right-aligns the characters in this string by padding
        ///     them on the right with a specified Ascii character, for a specified total length.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="totalWidth"></param>
        /// <param name="paddingChar"></param>
        /// <returns></returns>
        public static string PadLeftA(this string @this, int totalWidth, char paddingChar)
        {
            if (totalWidth < 0) throw new ArgumentOutOfRangeException(
                $"The argument `{nameof(totalWidth)}` must be an non-negative number.");

            var fillWidth = totalWidth - GetLengthA(@this);
            if (fillWidth > 0)
            {
                var fillBlank = false;
                int padWidth;

                if (paddingChar.GetLengthA() == 2)
                {
                    padWidth = @this.Length + fillWidth / 2;
                    fillBlank = fillWidth.IsOdd();
                }
                else padWidth = @this.Length + fillWidth;

                if (fillBlank)
                    return " " + @this.PadLeft(padWidth, paddingChar);
                else return @this.PadLeft(padWidth, paddingChar);
            }
            else return @this;
        }

        /// <summary>
        /// Returns a new string that left-aligns the characters in this string by padding
        ///     them on the right with a specified Ascii character, for a specified total length.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="totalWidth"></param>
        /// <returns></returns>
        public static string PadRightA(this string @this, int totalWidth) => PadRightA(@this, totalWidth, ' ');
        /// <summary>
        /// Returns a new string that left-aligns the characters in this string by padding
        ///     them on the right with a specified Ascii character, for a specified total length.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="totalWidth"></param>
        /// <param name="paddingChar"></param>
        /// <returns></returns>
        public static string PadRightA(this string @this, int totalWidth, char paddingChar)
        {
            if (totalWidth < 0) throw new ArgumentOutOfRangeException(
                $"Non-negative number required.{Environment.NewLine}" +
                $"Parameter name: totalWidth");

            var fillWidth = totalWidth - GetLengthA(@this);
            if (fillWidth > 0)
            {
                var fillBlank = false;
                int padWidth;

                if (paddingChar.GetLengthA() == 2)
                {
                    padWidth = @this.Length + fillWidth / 2;
                    fillBlank = fillWidth.IsOdd();
                }
                else padWidth = @this.Length + fillWidth;

                if (fillBlank)
                    return @this.PadRight(padWidth, paddingChar) + " ";
                else return @this.PadRight(padWidth, paddingChar);
            }
            else return @this;
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified element in this string.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int IndexOf(this string @this, Func<char, bool> predicate)
        {
            int i = 0;
            foreach (var e in @this)
            {
                if (predicate(e))
                    return i;
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified element in this string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="predicate"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        public static int IndexOf(this string @this, Func<char, bool> predicate, int startIndex)
        {
            int i = startIndex;
            foreach (var e in @this.Skip(startIndex))
            {
                if (predicate(e))
                    return i;
                i++;
            }
            return -1;
        }

        /// <summary>
        /// Reports the zero-based index of the first occurrence of the specified element in this string.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="predicate"></param>
        /// <param name="startIndex"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int IndexOf(this string @this, Func<char, bool> predicate, int startIndex, int count)
        {
            int i = startIndex;
            int iEnd = startIndex + count;
            if (iEnd < 1) return -1;

            foreach (var e in @this.Skip(startIndex))
            {
                if (predicate(e))
                    return i;
                i++;
                if (i == iEnd) break;
            }
            return -1;
        }

        /// <summary>
        /// Encodes all the characters in the specified string into a sequence of bytes(UTF-8), then returns it.
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] Bytes(this string @this) => Bytes(@this, Encoding.UTF8);

        /// <summary>
        /// Encodes all the characters in the specified string into a sequence of bytes, then returns it.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] Bytes(this string @this, Encoding encoding) => encoding.GetBytes(@this);

        /// <summary>
        /// Encodes all the characters in the specified string into a sequence of bytes, then returns it.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static byte[] Bytes(this string @this, string encoding) => Encoding.GetEncoding(encoding).GetBytes(@this);

    }
}
