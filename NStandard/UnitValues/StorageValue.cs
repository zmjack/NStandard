using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NStandard.UnitValues;

[Obsolete("Use StorageCapacity instead.")]
public struct StorageValue : IUnitValue, ISummable<StorageValue>
{
    public const string DefaultUnit = "b";

    public double BitValue { get; set; }
    public string Unit { get; set; } = DefaultUnit;
    public double Value => GetValue(Unit);

    public double GetValue(string unit)
    {
        unit ??= DefaultUnit;
        if (!IsValidUnit(unit)) throw new ArgumentException($"Invalid unit({unit}).", nameof(unit));
        return unit == DefaultUnit ? BitValue : BitValue / UnitLevelDict[unit];
    }

    public static readonly StorageValue Zero = new();

    public StorageValue()
    {
        BitValue = 0;
    }

    public StorageValue(double bit)
    {
        BitValue = bit;
    }

    public StorageValue(double value, string unit)
    {
        BitValue = value * UnitLevelDict[unit];
        Unit = unit;
    }

    private static readonly Regex _parseRegex = new(@"^(\d+|\.\d+|\d+\.\d*)\s*(\w+)$", RegexOptions.Singleline);

    public static StorageValue Parse(string s)
    {
        var match = _parseRegex.Match(s);
        if (!match.Success) throw new FormatException($"{nameof(StorageValue)} should contain number and unit(e.g. 1024 kb).");

        var groups = match.Groups;
        var number = double.Parse(groups[1].Value);
        var unit = groups[2].Value;
        return new(number, unit);
    }

    private static readonly Dictionary<string, long> UnitLevelDict = new()
    {
        ["b"] = 1,
        ["kb"] = 1024,
        ["Kb"] = 1024,
        ["mb"] = (long)1024 * 1024,
        ["Mb"] = (long)1024 * 1024,
        ["gb"] = (long)1024 * 1024 * 1024,
        ["Gb"] = (long)1024 * 1024 * 1024,
        ["tb"] = (long)1024 * 1024 * 1024 * 1024,
        ["Tb"] = (long)1024 * 1024 * 1024 * 1024,
        ["pb"] = (long)1024 * 1024 * 1024 * 1024 * 1024,
        ["Pb"] = (long)1024 * 1024 * 1024 * 1024 * 1024,

        ["B"] = 8,
        ["kB"] = (long)8 * 1024,
        ["KB"] = (long)8 * 1024,
        ["mB"] = (long)8 * 1024 * 1024,
        ["MB"] = (long)8 * 1024 * 1024,
        ["gB"] = (long)8 * 1024 * 1024 * 1024,
        ["GB"] = (long)8 * 1024 * 1024 * 1024,
        ["tB"] = (long)8 * 1024 * 1024 * 1024 * 1024,
        ["TB"] = (long)8 * 1024 * 1024 * 1024 * 1024,
        ["pB"] = (long)8 * 1024 * 1024 * 1024 * 1024 * 1024,
        ["PB"] = (long)8 * 1024 * 1024 * 1024 * 1024 * 1024,
    };

    public static bool IsValidUnit(string unit)
    {
        return UnitLevelDict.ContainsKey(unit);
    }

    public static StorageValue operator +(StorageValue operand) => new StorageValue(operand.BitValue).Unit(operand.Unit);
    public static StorageValue operator -(StorageValue operand) => new StorageValue(-operand.BitValue).Unit(operand.Unit);
    public static StorageValue operator +(StorageValue left, StorageValue right) => new StorageValue(left.BitValue + right.BitValue).Unit(left.Unit);
    public static StorageValue operator -(StorageValue left, StorageValue right) => new StorageValue(left.BitValue - right.BitValue).Unit(left.Unit);
    public static StorageValue operator *(double left, StorageValue right) => new StorageValue(left * right.BitValue).Unit(right.Unit);
    public static StorageValue operator *(StorageValue left, double right) => new StorageValue(left.BitValue * right).Unit(left.Unit);
    public static StorageValue operator /(StorageValue left, double right) => new StorageValue(left.BitValue / right).Unit(left.Unit);
    public static double operator /(StorageValue left, StorageValue right) => left.BitValue / right.BitValue;
    public static bool operator ==(StorageValue left, StorageValue right) => left.BitValue == right.BitValue;
    public static bool operator !=(StorageValue left, StorageValue right) => left.BitValue != right.BitValue;
    public static bool operator <=(StorageValue left, StorageValue right) => left.BitValue <= right.BitValue;
    public static bool operator <(StorageValue left, StorageValue right) => left.BitValue < right.BitValue;
    public static bool operator >(StorageValue left, StorageValue right) => left.BitValue > right.BitValue;
    public static bool operator >=(StorageValue left, StorageValue right) => left.BitValue >= right.BitValue;

    public override string ToString() => $"{Value} {Unit ?? DefaultUnit}";
    public string ToString(string unit) => $"{GetValue(unit)} {unit}";

    public void QuickSum(IEnumerable<StorageValue> values)
    {
        using var enumerator = values.GetEnumerator();
        while (enumerator.MoveNext())
        {
            double current = enumerator.Current.BitValue;
            double sum = current;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current.BitValue;
                sum += current;
            }

            BitValue = sum;
            Unit = values.First().Unit;
            return;
        }

        BitValue = default;
        Unit = DefaultUnit;
    }

    public void QuickAverage(IEnumerable<StorageValue> values)
    {
        using var enumerator = values.GetEnumerator();
        while (enumerator.MoveNext())
        {
            double current = enumerator.Current.BitValue;
            double sum = current;
            var count = 1;

            while (enumerator.MoveNext())
            {
                current = enumerator.Current.BitValue;
                sum += current;
                count++;
            }

            BitValue = sum / count;
            Unit = values.First().Unit;
            return;
        }

        BitValue = default;
        Unit = DefaultUnit;
    }
}
