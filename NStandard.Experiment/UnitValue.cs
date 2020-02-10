using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;

namespace NStandard
{
    public static class UnitValue
    {
        public static UnitValue<TValue> Create<TValue>(TValue value, string unit) where TValue : unmanaged => new UnitValue<TValue>(value, unit);
    }

    public struct UnitValue<TValue> where TValue : unmanaged
    {
        public TValue Value { get; private set; }
        public string Unit { get; private set; }

        public UnitValue(TValue value, string unit)
        {
            Value = value;
            Unit = unit;
        }

        public static UnitValue<TValue> operator +(UnitValue<TValue> left, UnitValue<TValue> right)
        {
            if (left.Unit != right.Unit)
                throw new ArgumentException($"All the `{nameof(Unit)}` must be same.");

            return new UnitValue<TValue>(Dynamic.AddChecked<TValue>(left.Value, right.Value), left.Unit);
        }

        public static UnitValue<TValue> operator *(UnitValue<TValue> left, UnitValue<TValue> right)
        {
            var units = left.Unit.ProjectToArray(new Regex(@"^((?:\*|/)?\w+)*$"));
            throw new NotImplementedException();

            //var units = EnumerableEx.Concat(left.Units.Select(x =>))
            //Array.Copy(left.Units, 0, units, 0, left.Units.Length);
            //Array.Copy(right.Units, 0, units, left.Units.Length, right.Units.Length);

            //return new UnitValue<TValue>(Dynamic.MultiplyChecked<TValue>(left.Value, right.Value), units);
        }

        public static UnitValue<TValue> operator /(UnitValue<TValue> left, UnitValue<TValue> right)
        {
            throw new NotImplementedException();

            //var units = new string[left.Units.Length + right.Units.Length];
            //Array.Copy(left.Units, 0, units, 0, left.Units.Length);
            //for (int i = 0; i < right.Units.Length; i++)
            //    units[left.Units.Length + i] = $"/{right.Units[i]}";

            //return new UnitValue<TValue>(Dynamic.Divide<TValue>(left.Value, right.Value), units);
        }

        public override string ToString() => $"{Value} {Unit}";

    }
}
