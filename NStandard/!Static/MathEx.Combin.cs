using NStandard.Schema;

namespace NStandard.Static;

public static partial class MathEx
{
	private static class Combination
	{
		private static int _maxNumber = -1;
		private static byte[] _binary = [];

		public static int MaxNumber
		{
			get => _maxNumber;
			private set
			{
				_binary = Generate(_maxNumber + 1, value);
				_maxNumber = value;
			}
		}

		static Combination()
		{
			MaxNumber = 20;
		}

		public static long Combin(int number, int chosen)
		{
			if (chosen < 0) throw new ArgumentException("The choice must be non-negative.", nameof(chosen));
			if (number < chosen) throw new ArgumentException("The total must be greater than or equal to the choice.", nameof(chosen));

			if (number > _maxNumber)
			{
				MaxNumber = number;
			}

			var _chosen = number - chosen;
			if (_chosen < chosen) chosen = _chosen;

			var div = number / 2;
			var offset = (number & 1) == 0 ? 0 : div + 1;
			var index = div * (div + 1) + offset + chosen;
			return BitConverter.ToInt64(_binary, index * sizeof(long));
		}

		private static byte[] Generate(int start, int number)
		{
			var list = new List<byte>();
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
					list.AddRange(BitConverter.GetBytes(value));
				}
			}
			return [.. _binary, .. list];
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
