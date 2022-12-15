using System;
using System.Collections;
using System.Linq;

namespace NStandard
{
    public static partial class Any
    {
        private static ArgumentException Exception_IncompatibleRank(string paramName) => new("The lengths can not be incompatible with the array.", paramName);
        private static ArgumentException Exception_VariableMustBeArray(string paramName) => new($"The {paramName} must be an array.", paramName);

        /// <summary>
        /// Reallocates storage space for an array variable.
        /// </summary>
        /// <typeparam name="TArray"></typeparam>
        /// <param name="variable"></param>
        /// <param name="lengths"></param>
        /// <exception cref="ArgumentException"></exception>
        public static void ReDim<TArray>(ref TArray variable, params int[] lengths) where TArray : class, ICollection, IEnumerable, IList, IStructuralComparable, IStructuralEquatable, ICloneable
        {
            var type = variable.GetType();
            if (!type.IsArray) throw Exception_VariableMustBeArray(nameof(variable));

            var origin = variable as Array;
            if (lengths.Length != origin.Rank) throw Exception_IncompatibleRank(nameof(lengths));

            var elementType = type.GetElementType();
            var newArray = Array.CreateInstance(elementType, lengths);
            var originLengths = origin.GetLengths();

#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER
            var stepper = new IndicesStepper(0, Zip(originLengths, lengths).Select(pair => Math.Min(pair.Item1, pair.Item2)).ToArray());
#else
            var stepper = new IndicesStepper(0, Zip(originLengths, lengths, (Item1, Item2) => new { Item1, Item2 }).Select(pair => Math.Min(pair.Item1, pair.Item2)).ToArray());
#endif
            foreach (var indices in stepper)
            {
                newArray.SetValue(origin.GetValue(indices), indices);
            }
            variable = newArray as TArray;
        }
    }
}
