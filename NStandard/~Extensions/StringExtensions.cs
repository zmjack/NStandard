﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NStandard;

[EditorBrowsable(EditorBrowsableState.Never)]
public static class StringExtensions
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
    public static bool IsNullOrWhiteSpace(this string @this)
    {
#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET40_OR_GREATER
        return string.IsNullOrWhiteSpace(@this);
#else
        if (@this == null) return true;
        for (int i = 0; i < @this.Length; i++)
        {
            if (!char.IsWhiteSpace(@this[i])) return false;
        }
        return true;
#endif
    }

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
    /// Returns the number of char which in the specified string.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="ch"></param>
    /// <returns></returns>
    public static int Count(this string @this, char ch)
    {
        return @this.Count(x => x == ch);
    }

    /// <summary>
    /// Returns the number of substring which in the specified string.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="subString"></param>
    /// <param name="overlapping"></param>
    /// <returns></returns>
    public static int Count(this string @this, string subString, bool overlapping = false)
    {
        if (string.IsNullOrEmpty(subString)) throw new ArgumentException("The sub string can not be null or empty.", nameof(subString));
        if (subString.Length == 1) return Count(@this, subString[0]);

        var indices = @this.ToCharArray().Locates(subString.ToCharArray());
        if (overlapping) return indices.Count();

        var length = subString.Length;
        var preIndex = -length;
        var count = 0;
        foreach (var index in indices)
        {
            if (index - preIndex >= length)
            {
                count++;
                preIndex = index;
            }
        }
        return count;
    }

    /// <summary>
    /// Returns a new string which is normalized by the newline string of current environment.
    /// <para>If a string come from non-Unix platforms, then its NewLine is "\r\n".</para>
    /// <para>If a string come from Unix platforms, then its NewLine is "\n".</para>
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static string NormalizeNewLine(this string @this)
    {
        return Environment.NewLine switch
        {
            "\r\n" => new Regex("\r(?!\n)|(?<!\r)\n", RegexOptions.Singleline).Replace(@this, "\r\n"),
            "\n" => new Regex("\r\n|\r(?!\n)", RegexOptions.Singleline).Replace(@this, "\n"),
            "\r" => new Regex("\r\n|(?<!\r)\n", RegexOptions.Singleline).Replace(@this, "\n"),
            _ => throw new NotSupportedException(),
        };
    }

    /// <summary>
    /// Divides a string into multi-lines (ignore Empty or WhiteSpace). If the string is null, return string[0]. 
    /// (Perhaps you should set `normalizeNewLine` to true to convert the NewLine 
    ///     which is defined in other system into the current system's.)
    /// </summary>
    /// <param name="this"></param>
    /// <param name="normalizeNewLine"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetPureLines(this string @this, bool normalizeNewLine = false) => GetLines(@this, normalizeNewLine, true);

    /// <summary>
    /// Divides a string into multi-lines. If the string is null, return string[0]. 
    /// (Perhaps you should set `normalizeNewLine` to true to convert the NewLine 
    ///     which is defined in other system into the current system's.)
    /// </summary>
    /// <param name="this"></param>
    /// <param name="normalizeNewLine"></param>
    /// <returns></returns>
    public static IEnumerable<string> GetLines(this string @this, bool normalizeNewLine = false) => GetLines(@this, normalizeNewLine, false);

    private static IEnumerable<string> GetLines(this string @this, bool normalizeNewLine = false, bool ignoreEmptyOrWhiteSpace = false)
    {
        if (normalizeNewLine) @this = @this.NormalizeNewLine();

        if (@this != null)
        {
            var newLineLength = Environment.NewLine.Length;
            var startIndex = 0;

            int findIndex;
            while ((findIndex = @this.IndexOf(Environment.NewLine, startIndex)) >= 0)
            {
                var line = @this.Slice(startIndex, findIndex);
                startIndex = findIndex + newLineLength;
                if (!(ignoreEmptyOrWhiteSpace && IsNullOrWhiteSpace(line)))
                    yield return line;
            }

            if (startIndex != @this.Length)
            {
                var line = @this.Slice(startIndex, @this.Length);
                if (!(ignoreEmptyOrWhiteSpace && IsNullOrWhiteSpace(line)))
                    yield return line;
            }
        }
    }

    private static readonly Regex UniqueRegex = new(@"[\s]{2,}");
    /// <summary>
    /// Removes all leading and trailing white-space characters from the current string,
    ///     and replaces multiple spaces with a single.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    public static string Unique(this string @this)
    {
        if (@this is null) return null;
        return UniqueRegex.Replace(@this.NormalizeNewLine().Replace(Environment.NewLine, " ").Trim(), " ");
    }

    /// <summary>
    /// In a specified input string, replaces all strings that match a regular expression
    //      pattern with a specified replacement string.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="regex"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public static string RegexReplace(this string @this, Regex regex, string replacement) => regex.Replace(@this, replacement);

    /// <summary>
    /// In a specified input string, replaces all strings that match a regular expression
    //      pattern with a specified replacement string.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="regex"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public static string RegexReplace(this string @this, Regex regex, MatchEvaluator evaluator) => regex.Replace(@this, evaluator);

    /// <summary>
    /// Extract strings by using regular expressions.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="regex"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public static IEnumerable<string> Extract(this string @this, Regex regex, string replacement = null)
    {
        if (@this is null) yield break;

        var startat = 0;
        while (true)
        {
            var match = regex.Match(@this, startat);
            if (match.Success)
            {
                if (replacement is null)
                {
                    if (match.Groups.Count > 1)
                        yield return string.Join("", match.Groups.OfType<Group>().Skip(1).Select(g => g.Value).ToArray());
                    else yield return match.Groups[0].Value;
                }
                else yield return regex.Replace(match.Groups[0].Value, replacement);

                startat = match.Index + Math.Max(1, match.Length);
                if (startat > @this.Length) break;
            }
            else break;
        }
    }

    /// <summary>
    /// Extract string by using regular expressions. If no match, return null.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="regex"></param>
    /// <param name="replacement"></param>
    /// <returns></returns>
    public static string ExtractFirst(this string @this, Regex regex, string replacement = null)
    {
        return Extract(@this, regex, replacement).FirstOrDefault();
    }

    /// <summary>
    /// Projects the specified string to an array by using regular expressions.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="regex"></param>
    /// <returns></returns>
    public static string[][] Resolve(this string @this, Regex regex)
    {
        if (TryResolve(@this, regex, out var ret)) return ret;
        else throw new ArgumentNullException("Can not match the sepecifed Regex.");
    }

    /// <summary>
    /// Projects the specified string to an array by using regular expressions.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="regex"></param>
    /// <returns></returns>
    public static bool TryResolve(this string @this, Regex regex, out string[][] ret)
    {
        var match = regex.Match(@this);
        if (match.Success)
        {
            ret = match.Groups.OfType<Group>()
                .Select(g => g.Captures.OfType<Capture>().Select(c => c.Value).ToArray()).ToArray();
            return true;
        }
        else
        {
            ret = null;
            return false;
        }
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
        {
            pos = @this.Length % unitLength;
            if (pos <= 0) pos = unitLength;
        }
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
    public static int GetLengthA(this string @this) => @this.Aggregate(0, (acc, cur) => acc += cur.GetLengthA());

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
        if (totalWidth < 0) throw new ArgumentOutOfRangeException($"The argument `{nameof(totalWidth)}` must be an non-negative number.");

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
        int end = startIndex + count;
        if (end < 1) return -1;

        foreach (var e in @this.Skip(startIndex))
        {
            if (predicate(e))
                return i;
            i++;
            if (i == end) break;
        }
        return -1;
    }

    /// <summary>
    /// Encodes all the characters in the specified string into a sequence of bytes(Unicode), then returns it.
    /// </summary>
    /// <param name="this"></param>
    /// <returns></returns>
    [Obsolete("Use Pipe(Encoding.Unicode.GetBytes) instead.")]
    public static byte[] Bytes(this string @this) => Bytes(@this, Encoding.Unicode);

    /// <summary>
    /// Encodes all the characters in the specified string into a sequence of bytes, then returns it.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    [Obsolete("Use Pipe(Encoding.*.GetBytes) instead.")]
    public static byte[] Bytes(this string @this, Encoding encoding) => encoding.GetBytes(@this);

    /// <summary>
    /// Encodes all the characters in the specified string into a sequence of bytes, then returns it.
    /// </summary>
    /// <param name="this"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    [Obsolete("Use Pipe(Encoding.GetEncoding(*).GetBytes) instead.")]
    public static byte[] Bytes(this string @this, string encoding) => Encoding.GetEncoding(encoding).GetBytes(@this);

}
