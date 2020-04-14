using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NStandard
{
    public class Loop : IEnumerable<int[]>
    {
        public int[] Lengths;

        public Loop(params int[] lengths)
        {
            Lengths = lengths;
        }

        IEnumerable<int[]> Enumerable()
        {
            var length = Lengths.Length;
            var iterator = new int[length];
            var lastIndex = length - 1;

            bool normalize(int pos)
            {
                if (iterator[pos] >= Lengths[pos])
                {
                    if (pos == 0) return false;

                    iterator[pos - 1] += iterator[pos] / Lengths[pos];
                    iterator[pos] = iterator[pos] % Lengths[pos];
                    return normalize(pos - 1);
                }
                else return true;
            }

            for (; normalize(lastIndex); iterator[lastIndex]++)
            {
                var item = new int[length];
                Array.Copy(iterator, item, length);
                yield return item;
            }
        }


        public IEnumerator<int[]> GetEnumerator() => Enumerable().GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
