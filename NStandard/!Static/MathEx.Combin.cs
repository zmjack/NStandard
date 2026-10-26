using NStandard.Schema;

namespace NStandard.Static;

public static partial class MathEx
{
    public static class Combination
    {
        private static int _optimized = 0;
        private static byte[] _binary = [.. BitConverter.GetBytes(1L)];

        public static int Optimized
        {
            get => _optimized;
            set
            {
                if (value < 1) throw new ArgumentException("The optimized number must be greater than 0.", nameof(value));
                if (value == _optimized) return;
                if (value > _optimized)
                {
                    var binary = new byte[GetTerm(value) * sizeof(long)];
                    Array.Copy(_binary, binary, _binary.Length);
                    var index = GetTerm(_optimized) * sizeof(long);
                    foreach (var b in Generate(_optimized + 1, value))
                    {
                        binary[index++] = b;
                    }
                    _binary = binary;
                }
                else
                {
                    var binary = new byte[GetTerm(value) * sizeof(long)];
                    Array.Copy(_binary, binary, binary.Length);
                    _binary = binary;
                }
                _optimized = value;
            }
        }
        public static int OptimizedLength => _binary.Length;

        static Combination()
        {
            Optimized = 20;
        }

        internal static long Combin(int number, int chosen)
        {
            if (chosen < 0) throw new ArgumentException("The chosen must be non-negative.", nameof(chosen));
            if (number < chosen) throw new ArgumentException("The number must be greater than or equal to the chosen.", nameof(chosen));

            if (number <= _optimized)
            {
                var _chosen = number - chosen;
                if (_chosen < chosen) chosen = _chosen;

                var div = number / 2;
                var offset = (number & 1) == 0 ? 0 : div + 1;
                var index = div * (div + 1) + offset + chosen;
                return BitConverter.ToInt64(_binary, index * sizeof(long));
            }
            else
            {
                var dp = new CombinDpContainer();
                return dp[StructTuple.Create(number, chosen)];
            }
        }

        private static int GetTerm(int number)
        {
            if (number % 2 == 0)
            {
                int k = number / 2 + 1;
                return k * k;
            }
            else
            {
                int k = (number + 1) / 2;
                return k * (k + 1);
            }
        }

        private static IEnumerable<byte> Generate(int start, int number)
        {
            var dp = new CombinDpContainer();
            /*
			 * 	s	i	| n\k	|	0	1	2	3	4	5	6	7	8
			 *	+0	0	| 0		|	1'								
			 *	+1	1	| 1		|	1'	1							
			 *	+1	2	| 2		|	1	2'	1						
			 *	+2	4	| 3		|	1	3'	3	1					
			 *	+2	6	| 4		|	1	4	6'	4	1				
			 *	+3	9	| 5		|	1	5	10'	10	5	1			
			 *	+3	12	| 6		|	1	6	15	20'	15	6	1		
			 *	+4	16	| 7		|	1	7	21	35'	35	21	7	1	
			 *	+4	20	| 8		|	1	8	28	56	70'	56	28	8	1
			 */
            for (int n = start; n <= number; n++)
            {
                for (int k = 0; k <= n / 2; k++)
                {
                    var value = dp[StructTuple.Create(n, k)];
                    foreach (var b in BitConverter.GetBytes(value))
                    {
                        yield return b;
                    }
                }
            }
        }

        private class CombinDpContainer : DpContainer<StructTuple<int, int>, long>
        {
            public override long StateTransfer(StructTuple<int, int> param)
            {
                var (number, chosen) = param;
                var _chosen = number - chosen;
                if (_chosen < chosen) chosen = _chosen;

                if (chosen == 1) return number;
                if (chosen == 0) return 1;
                if (chosen == number) return 1;

                //return MathEx.Permut(number, chosen) / MathEx.Permut(chosen, chosen);
                return this[StructTuple.Create(number - 1, chosen - 1)] + this[StructTuple.Create(number - 1, chosen)];
            }
        }
    }

    /// <summary>
    /// Returns the number of combinations for a given number of items.
    /// </summary>
    /// <param name="number"> The number of items. </param>
    /// <param name="chosen"> The number of items in each combination. </param>
    /// <returns></returns>
    public static long Combin(int number, int chosen)
    {
        return Combination.Combin(number, chosen);
    }
}
