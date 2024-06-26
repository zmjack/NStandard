﻿// <auto-generated/>
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using static NStandard.Measures.Length;

namespace NStandard.Measures;

#pragma warning disable CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.
public static class Length
{
	public struct mm : IMeasurable<decimal>
	{
		public decimal Value { get; set; }

        public mm(decimal value) => Value = value;
        public mm(short value) => Value = (decimal)value;
        public mm(int value) => Value = (decimal)value;
        public mm(long value) => Value = (decimal)value;
        public mm(ushort value) => Value = (decimal)value;
        public mm(uint value) => Value = (decimal)value;
        public mm(ulong value) => Value = (decimal)value;
        public mm(float value) => Value = (decimal)value;
        public mm(double value) => Value = (decimal)value;

		public static mm operator +(mm left, mm right) => new(left.Value + right.Value);
		public static mm operator -(mm left, mm right) => new(left.Value - right.Value);
		public static mm operator *(mm left, decimal right) => new(left.Value * right);
		public static mm operator /(mm left, decimal right) => new(left.Value / right);
		public static mm operator /(mm left, mm right) => left.Value / right.Value;
        
		public static bool operator ==(mm left, mm right) => left.Value == right.Value;
		public static bool operator !=(mm left, mm right) => left.Value != right.Value;
		public static bool operator <(mm left, mm right) => left.Value < right.Value;
		public static bool operator <=(mm left, mm right) => left.Value <= right.Value;
		public static bool operator >(mm left, mm right) => left.Value > right.Value;
		public static bool operator >=(mm left, mm right) => left.Value >= right.Value;
        
		public static explicit operator mm(cm other) => new(other.Value * 10);
		public static explicit operator mm(dm other) => new(other.Value * 100);
		public static explicit operator mm(m other) => new(other.Value * 1000);
		public static explicit operator mm(km other) => new(other.Value * 1000000);
        public static implicit operator mm(decimal value) => new mm(value);
        public static implicit operator mm(short value) => new((decimal)value);
        public static implicit operator mm(int value) => new((decimal)value);
        public static implicit operator mm(long value) => new((decimal)value);
        public static implicit operator mm(ushort value) => new((decimal)value);
        public static implicit operator mm(uint value) => new((decimal)value);
        public static implicit operator mm(ulong value) => new((decimal)value);
        public static implicit operator mm(float value) => new((decimal)value);
        public static implicit operator mm(double value) => new((decimal)value);

        public override bool Equals(object obj)
        {
            if (obj is not mm other) return false;
            return Value == other.Value;
        }
        public override int GetHashCode() => (int)(Value % int.MaxValue);
		public override string ToString() => $"{Value} mm";
	}

	public struct cm : IMeasurable<decimal>
	{
		public decimal Value { get; set; }

        public cm(decimal value) => Value = value;
        public cm(short value) => Value = (decimal)value;
        public cm(int value) => Value = (decimal)value;
        public cm(long value) => Value = (decimal)value;
        public cm(ushort value) => Value = (decimal)value;
        public cm(uint value) => Value = (decimal)value;
        public cm(ulong value) => Value = (decimal)value;
        public cm(float value) => Value = (decimal)value;
        public cm(double value) => Value = (decimal)value;

		public static cm operator +(cm left, cm right) => new(left.Value + right.Value);
		public static cm operator -(cm left, cm right) => new(left.Value - right.Value);
		public static cm operator *(cm left, decimal right) => new(left.Value * right);
		public static cm operator /(cm left, decimal right) => new(left.Value / right);
		public static cm operator /(cm left, cm right) => left.Value / right.Value;
        
		public static bool operator ==(cm left, cm right) => left.Value == right.Value;
		public static bool operator !=(cm left, cm right) => left.Value != right.Value;
		public static bool operator <(cm left, cm right) => left.Value < right.Value;
		public static bool operator <=(cm left, cm right) => left.Value <= right.Value;
		public static bool operator >(cm left, cm right) => left.Value > right.Value;
		public static bool operator >=(cm left, cm right) => left.Value >= right.Value;
        
		public static explicit operator cm(mm other) => new(other.Value / 10);
		public static explicit operator cm(dm other) => new(other.Value * 10);
		public static explicit operator cm(m other) => new(other.Value * 100);
		public static explicit operator cm(km other) => new(other.Value * 100000);
        public static implicit operator cm(decimal value) => new cm(value);
        public static implicit operator cm(short value) => new((decimal)value);
        public static implicit operator cm(int value) => new((decimal)value);
        public static implicit operator cm(long value) => new((decimal)value);
        public static implicit operator cm(ushort value) => new((decimal)value);
        public static implicit operator cm(uint value) => new((decimal)value);
        public static implicit operator cm(ulong value) => new((decimal)value);
        public static implicit operator cm(float value) => new((decimal)value);
        public static implicit operator cm(double value) => new((decimal)value);

        public override bool Equals(object obj)
        {
            if (obj is not cm other) return false;
            return Value == other.Value;
        }
        public override int GetHashCode() => (int)(Value % int.MaxValue);
		public override string ToString() => $"{Value} cm";
	}

	public struct dm : IMeasurable<decimal>
	{
		public decimal Value { get; set; }

        public dm(decimal value) => Value = value;
        public dm(short value) => Value = (decimal)value;
        public dm(int value) => Value = (decimal)value;
        public dm(long value) => Value = (decimal)value;
        public dm(ushort value) => Value = (decimal)value;
        public dm(uint value) => Value = (decimal)value;
        public dm(ulong value) => Value = (decimal)value;
        public dm(float value) => Value = (decimal)value;
        public dm(double value) => Value = (decimal)value;

		public static dm operator +(dm left, dm right) => new(left.Value + right.Value);
		public static dm operator -(dm left, dm right) => new(left.Value - right.Value);
		public static dm operator *(dm left, decimal right) => new(left.Value * right);
		public static dm operator /(dm left, decimal right) => new(left.Value / right);
		public static dm operator /(dm left, dm right) => left.Value / right.Value;
        
		public static bool operator ==(dm left, dm right) => left.Value == right.Value;
		public static bool operator !=(dm left, dm right) => left.Value != right.Value;
		public static bool operator <(dm left, dm right) => left.Value < right.Value;
		public static bool operator <=(dm left, dm right) => left.Value <= right.Value;
		public static bool operator >(dm left, dm right) => left.Value > right.Value;
		public static bool operator >=(dm left, dm right) => left.Value >= right.Value;
        
		public static explicit operator dm(mm other) => new(other.Value / 100);
		public static explicit operator dm(cm other) => new(other.Value / 10);
		public static explicit operator dm(m other) => new(other.Value * 10);
		public static explicit operator dm(km other) => new(other.Value * 10000);
        public static implicit operator dm(decimal value) => new dm(value);
        public static implicit operator dm(short value) => new((decimal)value);
        public static implicit operator dm(int value) => new((decimal)value);
        public static implicit operator dm(long value) => new((decimal)value);
        public static implicit operator dm(ushort value) => new((decimal)value);
        public static implicit operator dm(uint value) => new((decimal)value);
        public static implicit operator dm(ulong value) => new((decimal)value);
        public static implicit operator dm(float value) => new((decimal)value);
        public static implicit operator dm(double value) => new((decimal)value);

        public override bool Equals(object obj)
        {
            if (obj is not dm other) return false;
            return Value == other.Value;
        }
        public override int GetHashCode() => (int)(Value % int.MaxValue);
		public override string ToString() => $"{Value} dm";
	}

	public struct m : IMeasurable<decimal>
	{
		public decimal Value { get; set; }

        public m(decimal value) => Value = value;
        public m(short value) => Value = (decimal)value;
        public m(int value) => Value = (decimal)value;
        public m(long value) => Value = (decimal)value;
        public m(ushort value) => Value = (decimal)value;
        public m(uint value) => Value = (decimal)value;
        public m(ulong value) => Value = (decimal)value;
        public m(float value) => Value = (decimal)value;
        public m(double value) => Value = (decimal)value;

		public static m operator +(m left, m right) => new(left.Value + right.Value);
		public static m operator -(m left, m right) => new(left.Value - right.Value);
		public static m operator *(m left, decimal right) => new(left.Value * right);
		public static m operator /(m left, decimal right) => new(left.Value / right);
		public static m operator /(m left, m right) => left.Value / right.Value;
        
		public static bool operator ==(m left, m right) => left.Value == right.Value;
		public static bool operator !=(m left, m right) => left.Value != right.Value;
		public static bool operator <(m left, m right) => left.Value < right.Value;
		public static bool operator <=(m left, m right) => left.Value <= right.Value;
		public static bool operator >(m left, m right) => left.Value > right.Value;
		public static bool operator >=(m left, m right) => left.Value >= right.Value;
        
		public static explicit operator m(mm other) => new(other.Value / 1000);
		public static explicit operator m(cm other) => new(other.Value / 100);
		public static explicit operator m(dm other) => new(other.Value / 10);
		public static explicit operator m(km other) => new(other.Value * 1000);
        public static implicit operator m(decimal value) => new m(value);
        public static implicit operator m(short value) => new((decimal)value);
        public static implicit operator m(int value) => new((decimal)value);
        public static implicit operator m(long value) => new((decimal)value);
        public static implicit operator m(ushort value) => new((decimal)value);
        public static implicit operator m(uint value) => new((decimal)value);
        public static implicit operator m(ulong value) => new((decimal)value);
        public static implicit operator m(float value) => new((decimal)value);
        public static implicit operator m(double value) => new((decimal)value);

        public override bool Equals(object obj)
        {
            if (obj is not m other) return false;
            return Value == other.Value;
        }
        public override int GetHashCode() => (int)(Value % int.MaxValue);
		public override string ToString() => $"{Value} m";
	}

	public struct km : IMeasurable<decimal>
	{
		public decimal Value { get; set; }

        public km(decimal value) => Value = value;
        public km(short value) => Value = (decimal)value;
        public km(int value) => Value = (decimal)value;
        public km(long value) => Value = (decimal)value;
        public km(ushort value) => Value = (decimal)value;
        public km(uint value) => Value = (decimal)value;
        public km(ulong value) => Value = (decimal)value;
        public km(float value) => Value = (decimal)value;
        public km(double value) => Value = (decimal)value;

		public static km operator +(km left, km right) => new(left.Value + right.Value);
		public static km operator -(km left, km right) => new(left.Value - right.Value);
		public static km operator *(km left, decimal right) => new(left.Value * right);
		public static km operator /(km left, decimal right) => new(left.Value / right);
		public static km operator /(km left, km right) => left.Value / right.Value;
        
		public static bool operator ==(km left, km right) => left.Value == right.Value;
		public static bool operator !=(km left, km right) => left.Value != right.Value;
		public static bool operator <(km left, km right) => left.Value < right.Value;
		public static bool operator <=(km left, km right) => left.Value <= right.Value;
		public static bool operator >(km left, km right) => left.Value > right.Value;
		public static bool operator >=(km left, km right) => left.Value >= right.Value;
        
		public static explicit operator km(mm other) => new(other.Value / 1000000);
		public static explicit operator km(cm other) => new(other.Value / 100000);
		public static explicit operator km(dm other) => new(other.Value / 10000);
		public static explicit operator km(m other) => new(other.Value / 1000);
        public static implicit operator km(decimal value) => new km(value);
        public static implicit operator km(short value) => new((decimal)value);
        public static implicit operator km(int value) => new((decimal)value);
        public static implicit operator km(long value) => new((decimal)value);
        public static implicit operator km(ushort value) => new((decimal)value);
        public static implicit operator km(uint value) => new((decimal)value);
        public static implicit operator km(ulong value) => new((decimal)value);
        public static implicit operator km(float value) => new((decimal)value);
        public static implicit operator km(double value) => new((decimal)value);

        public override bool Equals(object obj)
        {
            if (obj is not km other) return false;
            return Value == other.Value;
        }
        public override int GetHashCode() => (int)(Value % int.MaxValue);
		public override string ToString() => $"{Value} km";
	}

}

[EditorBrowsable(EditorBrowsableState.Never)]
public static class LengthExtensions
{
    public static mm Sum(this IEnumerable<mm> @this) => new mm(@this.Sum(x => x.Value));
    public static mm Average(this IEnumerable<mm> @this) => new mm(@this.Average(x => x.Value));

    public static cm Sum(this IEnumerable<cm> @this) => new cm(@this.Sum(x => x.Value));
    public static cm Average(this IEnumerable<cm> @this) => new cm(@this.Average(x => x.Value));

    public static dm Sum(this IEnumerable<dm> @this) => new dm(@this.Sum(x => x.Value));
    public static dm Average(this IEnumerable<dm> @this) => new dm(@this.Average(x => x.Value));

    public static m Sum(this IEnumerable<m> @this) => new m(@this.Sum(x => x.Value));
    public static m Average(this IEnumerable<m> @this) => new m(@this.Average(x => x.Value));

    public static km Sum(this IEnumerable<km> @this) => new km(@this.Sum(x => x.Value));
    public static km Average(this IEnumerable<km> @this) => new km(@this.Average(x => x.Value));

}
#pragma warning restore CS8981 // The type name only contains lower-cased ascii characters. Such names may become reserved for the language.

