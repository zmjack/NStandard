using System.Collections.Generic;

namespace NStandard.Algorithms
{
    public class Kmp
    {
        public string Pattern { get; private set; }
        public int[] PartialMoves { get; private set; }

        public Kmp(string pattern)
        {
            Pattern = pattern;
            PartialMoves = new int[pattern.Length];
            AnalysisPattern();
        }

        private void AnalysisPattern()
        {
            PartialMoves[0] = 1;
            for (int i = 1; i < Pattern.Length; i++)
            {
                var part = Pattern.Slice(0, i + 1);
                var prefixs = GetPrefixs(part);
                var suffixs = GetSuffixs(part);

                for (int j = 0; j < prefixs.Length; j++)
                {
                    if (prefixs[j] == suffixs[j])
                    {
                        PartialMoves[i] = j;
                        break;
                    }
                }

                PartialMoves[i]++;
            }
        }

        private string[] GetPrefixs(string input)
        {
            var ret = new Stack<string>();
            for (int i = 1; i < input.Length; i++)
                ret.Push(input.Slice(0, i));
            return ret.ToArray();
        }

        private string[] GetSuffixs(string input)
        {
            var ret = new Stack<string>();
            for (int i = 1; i < input.Length; i++)
                ret.Push(input.Slice(-i, input.Length));
            return ret.ToArray();
        }

        public unsafe int Count(string source, bool repeatMatchChars = false)
        {
            int findCount = 0;

            fixed (char* pSource = source)
            {
                char* p = pSource;
                char* pEnd = pSource + source.Length;
                while (p < pEnd)
                {
                    var find = true;
                    for (int i = 0; i < Pattern.Length; i++)
                    {
                        if (*(p + i) != Pattern[i])
                        {
                            p += PartialMoves[i];
                            find = false;
                            break;
                        }
                    }
                    if (find)
                    {
                        findCount++;
                        p += repeatMatchChars ? 1 : Pattern.Length;
                    }
                }
            }

            return findCount;
        }

    }
}
