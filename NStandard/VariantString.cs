using System;

namespace NStandard
{
    public class VariantString
    {
        public string String;

        public VariantString(string str) => String = str ?? string.Empty;

        public VariantString(char obj) => String = obj.ToString();
        public VariantString(byte obj) => String = obj.ToString();
        public VariantString(sbyte obj) => String = obj.ToString();
        public VariantString(short obj) => String = obj.ToString();
        public VariantString(ushort obj) => String = obj.ToString();
        public VariantString(int obj) => String = obj.ToString();
        public VariantString(uint obj) => String = obj.ToString();
        public VariantString(long obj) => String = obj.ToString();
        public VariantString(ulong obj) => String = obj.ToString();
        public VariantString(float obj) => String = obj.ToString();
        public VariantString(double obj) => String = obj.ToString();
        public VariantString(decimal obj) => String = obj.ToString();
        public VariantString(DateTime obj) => String = obj.ToString();
        public VariantString(bool obj) => String = obj.ToString();
        public VariantString(Guid obj) => String = obj.ToString();
#if NET6_0_OR_GREATER
        public VariantString(DateOnly obj) => String = obj.ToString();
        public VariantString(TimeOnly obj) => String = obj.ToString();
#endif

        public VariantString(char? obj) => String = obj?.ToString();
        public VariantString(byte? obj) => String = obj?.ToString();
        public VariantString(sbyte? obj) => String = obj?.ToString();
        public VariantString(short? obj) => String = obj?.ToString();
        public VariantString(ushort? obj) => String = obj?.ToString();
        public VariantString(int? obj) => String = obj?.ToString();
        public VariantString(uint? obj) => String = obj?.ToString();
        public VariantString(long? obj) => String = obj?.ToString();
        public VariantString(ulong? obj) => String = obj?.ToString();
        public VariantString(float? obj) => String = obj?.ToString();
        public VariantString(double? obj) => String = obj?.ToString();
        public VariantString(decimal? obj) => String = obj?.ToString();
        public VariantString(DateTime? obj) => String = obj?.ToString();
        public VariantString(bool? obj) => String = obj?.ToString();
        public VariantString(Guid? obj) => String = obj?.ToString();
#if NET6_0_OR_GREATER
        public VariantString(DateOnly? obj) => String = obj?.ToString();
        public VariantString(TimeOnly? obj) => String = obj?.ToString();
#endif

        public static implicit operator VariantString(string str) => new(str);
        public static implicit operator string(VariantString @this) => @this.String;

        public static implicit operator VariantString(char obj) => new(obj);
        public static implicit operator VariantString(byte obj) => new(obj);
        public static implicit operator VariantString(sbyte obj) => new(obj);
        public static implicit operator VariantString(short obj) => new(obj);
        public static implicit operator VariantString(ushort obj) => new(obj);
        public static implicit operator VariantString(int obj) => new(obj);
        public static implicit operator VariantString(uint obj) => new(obj);
        public static implicit operator VariantString(long obj) => new(obj);
        public static implicit operator VariantString(ulong obj) => new(obj);
        public static implicit operator VariantString(float obj) => new(obj);
        public static implicit operator VariantString(double obj) => new(obj);
        public static implicit operator VariantString(decimal obj) => new(obj);
        public static implicit operator VariantString(DateTime obj) => new(obj);
        public static implicit operator VariantString(bool obj) => new(obj);
        public static implicit operator VariantString(Guid obj) => new(obj);
#if NET6_0_OR_GREATER
        public static implicit operator VariantString(DateOnly obj) => new(obj);
        public static implicit operator VariantString(TimeOnly obj) => new(obj);
#endif

        public static implicit operator VariantString(char? obj) => new(obj);
        public static implicit operator VariantString(byte? obj) => new(obj);
        public static implicit operator VariantString(short? obj) => new(obj);
        public static implicit operator VariantString(ushort? obj) => new(obj);
        public static implicit operator VariantString(int? obj) => new(obj);
        public static implicit operator VariantString(uint? obj) => new(obj);
        public static implicit operator VariantString(long? obj) => new(obj);
        public static implicit operator VariantString(ulong? obj) => new(obj);
        public static implicit operator VariantString(float? obj) => new(obj);
        public static implicit operator VariantString(double? obj) => new(obj);
        public static implicit operator VariantString(decimal? obj) => new(obj);
        public static implicit operator VariantString(DateTime? obj) => new(obj);
        public static implicit operator VariantString(bool? obj) => new(obj);
        public static implicit operator VariantString(Guid? obj) => new(obj);
#if NET6_0_OR_GREATER
        public static implicit operator VariantString(DateOnly? obj) => new(obj);
        public static implicit operator VariantString(TimeOnly? obj) => new(obj);
#endif

        public static implicit operator char(VariantString @this) => char.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator byte(VariantString @this) => byte.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator sbyte(VariantString @this) => sbyte.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator short(VariantString @this) => short.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator ushort(VariantString @this) => ushort.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator int(VariantString @this) => int.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator uint(VariantString @this) => uint.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator long(VariantString @this) => long.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator ulong(VariantString @this) => ulong.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator float(VariantString @this) => float.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator double(VariantString @this) => double.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator decimal(VariantString @this) => decimal.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator DateTime(VariantString @this) => DateTime.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator bool(VariantString @this)
        {
            if (@this.String.IsNullOrEmpty()) return false;
            if (double.TryParse(@this.String, out var b)) return b > 0;
            return bool.TryParse(@this.String, out var ret) && ret;
        }
#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET40_OR_GREATER
        public static implicit operator Guid(VariantString @this) => Guid.TryParse(@this.String, out var ret) ? ret : default;
#else
        public static implicit operator Guid(VariantString @this)
        {
            try { return new Guid(@this.String); }
            catch { return Guid.Empty; }
        }
#endif
#if NET6_0_OR_GREATER
        public static implicit operator DateOnly(VariantString @this) => DateOnly.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator TimeOnly(VariantString @this) => TimeOnly.TryParse(@this.String, out var ret) ? ret : default;
#endif

        public static implicit operator char?(VariantString @this) => char.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator byte?(VariantString @this) => byte.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator sbyte?(VariantString @this) => sbyte.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator short?(VariantString @this) => short.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator ushort?(VariantString @this) => ushort.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator int?(VariantString @this) => int.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator uint?(VariantString @this) => uint.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator long?(VariantString @this) => long.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator ulong?(VariantString @this) => ulong.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator float?(VariantString @this) => float.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator double?(VariantString @this) => double.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator decimal?(VariantString @this) => decimal.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator DateTime?(VariantString @this) => DateTime.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator bool?(VariantString @this)
        {
            if (@this.String.IsNullOrEmpty()) return null;
            if (double.TryParse(@this.String, out var b)) return b > 0;
            return bool.TryParse(@this.String, out var ret).For(x => x ? ret : (bool?)null);
        }
#if NET5_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET40_OR_GREATER
        public static implicit operator Guid?(VariantString @this) => Guid.TryParse(@this.String, out var ret).For(x => x ? ret : (Guid?)default);
#else
        public static implicit operator Guid?(VariantString @this)
        {
            try { return new Guid(@this.String); }
            catch { return null; }
        }
#endif
#if NET6_0_OR_GREATER
        public static implicit operator DateOnly?(VariantString @this) => DateOnly.TryParse(@this.String, out var ret) ? ret : default;
        public static implicit operator TimeOnly?(VariantString @this) => TimeOnly.TryParse(@this.String, out var ret) ? ret : default;
#endif

        public override bool Equals(object obj)
        {
            if (obj is VariantString vs) return ToString() == vs.ToString();

            switch (obj)
            {
                case char o: return ToString() == o.ToString();
                case byte o: return ToString() == o.ToString();
                case sbyte o: return ToString() == o.ToString();
                case short o: return ToString() == o.ToString();
                case ushort o: return ToString() == o.ToString();
                case int o: return ToString() == o.ToString();
                case uint o: return ToString() == o.ToString();
                case long o: return ToString() == o.ToString();
                case ulong o: return ToString() == o.ToString();
                case float o: return ToString() == o.ToString();
                case double o: return ToString() == o.ToString();
                case decimal o: return ToString() == o.ToString();
                case DateTime o: return ToString() == o.ToString();
                case bool o: return ToString() == o.ToString();
                case Guid o: return ToString() == o.ToString();
#if NET6_0_OR_GREATER
                case DateOnly o: return ToString() == o.ToString();
                case TimeOnly o: return ToString() == o.ToString();
#endif
            };

            switch (obj.GetType())
            {
                case Type type when type == typeof(char?): return ToString() == obj?.ToString();
                case Type type when type == typeof(byte?): return ToString() == obj?.ToString();
                case Type type when type == typeof(sbyte?): return ToString() == obj?.ToString();
                case Type type when type == typeof(short?): return ToString() == obj?.ToString();
                case Type type when type == typeof(ushort?): return ToString() == obj?.ToString();
                case Type type when type == typeof(int?): return ToString() == obj?.ToString();
                case Type type when type == typeof(uint?): return ToString() == obj?.ToString();
                case Type type when type == typeof(long?): return ToString() == obj?.ToString();
                case Type type when type == typeof(ulong?): return ToString() == obj?.ToString();
                case Type type when type == typeof(float?): return ToString() == obj?.ToString();
                case Type type when type == typeof(double?): return ToString() == obj?.ToString();
                case Type type when type == typeof(decimal?): return ToString() == obj?.ToString();
                case Type type when type == typeof(DateTime?): return ToString() == obj?.ToString();
                case Type type when type == typeof(bool?): return ToString() == obj?.ToString();
                case Type type when type == typeof(Guid?): return ToString() == obj?.ToString();
#if NET6_0_OR_GREATER
                case Type type when type == typeof(DateOnly?): return ToString() == obj?.ToString();
                case Type type when type == typeof(TimeOnly?): return ToString() == obj?.ToString();
#endif
            }

            return false;
        }

        public static bool operator ==(VariantString left, VariantString right) => left.ToString() == right.ToString();
        public static bool operator !=(VariantString left, VariantString right) => left.ToString() != right.ToString();

        public override string ToString() => String?.ToString();

    }
}
