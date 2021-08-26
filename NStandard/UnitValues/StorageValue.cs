using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NStandard.UnitValues
{
    public struct StorageValue : IUnitValue<StorageValue, string, double>
    {
        private const string BaseUnit = "b";

        public double OriginalValue { get; private set; }
        public string Unit { get; private set; }
        public double Value => Unit == BaseUnit ? OriginalValue : OriginalValue / UnitLevelDict[Unit];

        public static readonly StorageValue Zero = new StorageValue().Set(0, BaseUnit);

        public StorageValue(double value, string unit)
        {
            if (!IsValidUnit(unit)) throw new ArgumentException($"Invalid unit({unit}).", nameof(unit));
            OriginalValue = value * UnitLevelDict[unit];
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

        public StorageValue Set(double originalValue, string unit)
        {
            OriginalValue = originalValue;
            Unit = unit;
            return this;
        }

        public StorageValue Format(string targetUnit)
        {
            return new() { OriginalValue = OriginalValue, Unit = targetUnit };
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

        public static StorageValue operator +(StorageValue left, StorageValue right)
        {
            return new() { OriginalValue = left.OriginalValue + right.OriginalValue, Unit = left.Unit };
        }

        public static StorageValue operator -(StorageValue left, StorageValue right)
        {
            return new() { OriginalValue = left.OriginalValue - right.OriginalValue, Unit = left.Unit };
        }

        public static StorageValue operator *(double left, StorageValue right)
        {
            return new() { OriginalValue = left * right.OriginalValue, Unit = right.Unit };
        }

        public static StorageValue operator *(StorageValue left, double right)
        {
            return new() { OriginalValue = left.OriginalValue * right, Unit = left.Unit };
        }

        public static StorageValue operator /(StorageValue left, double right)
        {
            return new() { OriginalValue = left.OriginalValue / right, Unit = left.Unit };
        }

        public static double operator /(StorageValue left, StorageValue right)
        {
            return left.OriginalValue / right.OriginalValue;
        }

        public static bool operator ==(StorageValue left, StorageValue right)
        {
            return left.OriginalValue == right.OriginalValue;
        }

        public static bool operator !=(StorageValue left, StorageValue right)
        {
            return left.OriginalValue != right.OriginalValue;
        }

        public static bool operator <=(StorageValue left, StorageValue right)
        {
            return left.OriginalValue <= right.OriginalValue;
        }

        public static bool operator <(StorageValue left, StorageValue right)
        {
            return left.OriginalValue < right.OriginalValue;
        }

        public static bool operator >(StorageValue left, StorageValue right)
        {
            return left.OriginalValue > right.OriginalValue;
        }

        public static bool operator >=(StorageValue left, StorageValue right)
        {
            return left.OriginalValue >= right.OriginalValue;
        }

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }
    }
}
