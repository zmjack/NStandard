using System.Collections.Generic;

namespace NStandard.Analyzer.Extensions;

internal static class StringExtensions
{
    private static int GetCharPosition(ref string str, int pos) => pos < 0 ? str.Length + pos : pos;

    public static string Slice(this string @this, int start, int stop)
    {
        start = GetCharPosition(ref @this, start);
        stop = GetCharPosition(ref @this, stop);

        var length = stop - start;
        return @this.Substring(start, length);
    }

    public static IEnumerable<string> GetLines(this string @this)
    {
        if (@this != null)
        {
            var newLineLength = 2;
            var startIndex = 0;

            int findIndex;
            while ((findIndex = @this.IndexOf("\r\n", startIndex)) >= 0)
            {
                var line = @this.Slice(startIndex, findIndex);
                startIndex = findIndex + newLineLength;
                yield return line;
            }

            if (startIndex != @this.Length)
            {
                var line = @this.Slice(startIndex, @this.Length);
                yield return line;
            }
        }
    }
}
