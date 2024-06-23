using NStandard.Collections;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NStandard;

internal static partial class ArrayMapper
{
#if NET7_0_OR_GREATER
    [GeneratedRegex(@"(\[,*\])+", RegexOptions.Singleline)]
    private static partial Regex GetArrayTypeRanksRegex();
    private static readonly Regex ArrayTypeRanksRegex = GetArrayTypeRanksRegex();
#else
    private static readonly Regex ArrayTypeRanksRegex = new(@"(\[,*\])+", RegexOptions.Singleline);
#endif


    private static int[] GetReversedRanks(Type arrayType)
    {
        var forwards = Any.Forward(arrayType, x => x.GetElementType()!.UnderlyingSystemType);

        var match = ArrayTypeRanksRegex.Match(arrayType.FullName!);
        if (match.Success)
        {
            var captures = match.Groups[1].Captures;
#if NETCOREAPP2_0_OR_GREATER || NETSTANDARD2_1_OR_GREATER
            return captures.Select(x => x.Length - 1).ToArray();
#else
            return captures.OfType<Capture>().Select(x => x.Length - 1).ToArray();
#endif
        }
        else
        {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET46_OR_GREATER
            return Array.Empty<int>();
#else 
            return ArrayEx.Empty<int>();
#endif
        }
    }

    private static Type GetElementType(Type underlyingType, int[] reversedRanks)
    {
        var sb = new StringBuilder();
        sb.Append(underlyingType);
        foreach (var rank in reversedRanks)
        {
            sb.Append($"[{",".Repeat(rank - 1)}]");
        }
        var typeName = sb.ToString();
        return Type.GetType(typeName)!;
    }

    private static InvalidCastException Exception_TypeNotMatched(Type underlyingType, Type fromType)
    {
        return new($"Can not convert {underlyingType} to {fromType}.");
    }

    public static Array Map<TConvertFrom, TConvertTo>(Array source, Func<TConvertFrom, TConvertTo> convert)
    {
        var type = source.GetType();
        var ranks = GetReversedRanks(type);
        var elementTypes = Any.Forward(type, x => x.GetElementType()!);
        var underlyingType = elementTypes.Last();

        var fromType = typeof(TConvertFrom);
        if (underlyingType != fromType) throw Exception_TypeNotMatched(underlyingType, fromType);

        return InnerMap(source, ranks.Take(ranks.Length - 1).ToArray(), underlyingType, convert);
    }

    private static Array InnerMap<TConvertFrom, TConvertTo>(Array source, int[] elementReversedRanks, Type underlyingType, Func<TConvertFrom, TConvertTo> convert)
    {
        var convertType = typeof(TConvertTo);

        var type = source.GetType();
        var elementType = type.GetElementType();
        var sourceLengths = source.GetLengths();

        var target = Array.CreateInstance(GetElementType(convertType, elementReversedRanks), sourceLengths);
        var targetVisitor = new ArrayVisitor(target);
        var sourceVisitor = new ArrayVisitor(source);

        if (elementType != underlyingType)
        {
            foreach (var (index, value) in sourceVisitor.GetValues().Pairs())
            {
                var array = InnerMap((value as Array)!, elementReversedRanks.Take(elementReversedRanks.Length - 1).ToArray(), underlyingType, convert);
                targetVisitor.SetValue(array, index);
            }
        }
        else
        {
            foreach (var (index, value) in sourceVisitor.GetValues().Pairs())
            {
                var finalValue = convert((TConvertFrom)value!);
                targetVisitor.SetValue(finalValue, index);
            }
        }

        return target;
    }
}
