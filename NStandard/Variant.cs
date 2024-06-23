using System;

namespace NStandard;

public class Variant
{
    private readonly string? _string;

    public Variant(string? str) => _string = str ?? string.Empty;

    public Variant(char obj) => _string = obj.ToString();
    public Variant(byte obj) => _string = obj.ToString();
    public Variant(sbyte obj) => _string = obj.ToString();
    public Variant(short obj) => _string = obj.ToString();
    public Variant(ushort obj) => _string = obj.ToString();
    public Variant(int obj) => _string = obj.ToString();
    public Variant(uint obj) => _string = obj.ToString();
    public Variant(long obj) => _string = obj.ToString();
    public Variant(ulong obj) => _string = obj.ToString();
    public Variant(float obj) => _string = obj.ToString();
    public Variant(double obj) => _string = obj.ToString();
    public Variant(decimal obj) => _string = obj.ToString();
    public Variant(DateTime obj) => _string = obj.ToString();
    public Variant(bool obj) => _string = obj.ToString();
    public Variant(Guid obj) => _string = obj.ToString();
#if NET6_0_OR_GREATER
    public Variant(DateOnly obj) => _string = obj.ToString();
    public Variant(TimeOnly obj) => _string = obj.ToString();
#endif

    public Variant(char? obj) => _string = obj?.ToString();
    public Variant(byte? obj) => _string = obj?.ToString();
    public Variant(sbyte? obj) => _string = obj?.ToString();
    public Variant(short? obj) => _string = obj?.ToString();
    public Variant(ushort? obj) => _string = obj?.ToString();
    public Variant(int? obj) => _string = obj?.ToString();
    public Variant(uint? obj) => _string = obj?.ToString();
    public Variant(long? obj) => _string = obj?.ToString();
    public Variant(ulong? obj) => _string = obj?.ToString();
    public Variant(float? obj) => _string = obj?.ToString();
    public Variant(double? obj) => _string = obj?.ToString();
    public Variant(decimal? obj) => _string = obj?.ToString();
    public Variant(DateTime? obj) => _string = obj?.ToString();
    public Variant(bool? obj) => _string = obj?.ToString();
    public Variant(Guid? obj) => _string = obj?.ToString();
#if NET6_0_OR_GREATER
    public Variant(DateOnly? obj) => _string = obj?.ToString();
    public Variant(TimeOnly? obj) => _string = obj?.ToString();
#endif

    public static implicit operator Variant(string str) => new(str);
    public static implicit operator string?(Variant @this) => @this._string;

    public static implicit operator Variant(char obj) => new(obj);
    public static implicit operator Variant(byte obj) => new(obj);
    public static implicit operator Variant(sbyte obj) => new(obj);
    public static implicit operator Variant(short obj) => new(obj);
    public static implicit operator Variant(ushort obj) => new(obj);
    public static implicit operator Variant(int obj) => new(obj);
    public static implicit operator Variant(uint obj) => new(obj);
    public static implicit operator Variant(long obj) => new(obj);
    public static implicit operator Variant(ulong obj) => new(obj);
    public static implicit operator Variant(float obj) => new(obj);
    public static implicit operator Variant(double obj) => new(obj);
    public static implicit operator Variant(decimal obj) => new(obj);
    public static implicit operator Variant(DateTime obj) => new(obj);
    public static implicit operator Variant(bool obj) => new(obj);
    public static implicit operator Variant(Guid obj) => new(obj);
#if NET6_0_OR_GREATER
    public static implicit operator Variant(DateOnly obj) => new(obj);
    public static implicit operator Variant(TimeOnly obj) => new(obj);
#endif

    public static implicit operator Variant(char? obj) => new(obj);
    public static implicit operator Variant(byte? obj) => new(obj);
    public static implicit operator Variant(short? obj) => new(obj);
    public static implicit operator Variant(ushort? obj) => new(obj);
    public static implicit operator Variant(int? obj) => new(obj);
    public static implicit operator Variant(uint? obj) => new(obj);
    public static implicit operator Variant(long? obj) => new(obj);
    public static implicit operator Variant(ulong? obj) => new(obj);
    public static implicit operator Variant(float? obj) => new(obj);
    public static implicit operator Variant(double? obj) => new(obj);
    public static implicit operator Variant(decimal? obj) => new(obj);
    public static implicit operator Variant(DateTime? obj) => new(obj);
    public static implicit operator Variant(bool? obj) => new(obj);
    public static implicit operator Variant(Guid? obj) => new(obj);
#if NET6_0_OR_GREATER
    public static implicit operator Variant(DateOnly? obj) => new(obj);
    public static implicit operator Variant(TimeOnly? obj) => new(obj);
#endif

    public static implicit operator char(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (char.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator byte(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (byte.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator sbyte(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (sbyte.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator short(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (short.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator ushort(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (ushort.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator int(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (int.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator uint(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (uint.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator long(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (long.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator ulong(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (ulong.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator float(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (float.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator double(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (double.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator decimal(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (decimal.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator DateTime(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (DateTime.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator bool(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (double.TryParse(@this._string, out var b)) return b > 0;
        return bool.TryParse(@this._string, out var ret) && ret;
    }
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET451_OR_GREATER
    public static implicit operator Guid(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (Guid.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
#else
    public static implicit operator Guid(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        try { return new Guid(@this._string); }
        catch { return default; }
    }
#endif
#if NET6_0_OR_GREATER
    public static implicit operator DateOnly(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (DateOnly.TryParse(@this._string, out var date)) return date;
        if (DateTime.TryParse(@this._string, out var dt)) return DateOnly.FromDateTime(dt);
        return default;
    }
    public static implicit operator TimeOnly(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (TimeOnly.TryParse(@this._string, out var date)) return date;
        if (DateTime.TryParse(@this._string, out var dt)) return TimeOnly.FromDateTime(dt);
        return default;
    }
#endif

    public static implicit operator char?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (char.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator byte?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (byte.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator sbyte?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (sbyte.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator short?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (short.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator ushort?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (ushort.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator int?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (int.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator uint?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (uint.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator long?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (long.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator ulong?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (ulong.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator float?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (float.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator double?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (double.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator decimal?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (decimal.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator DateTime?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (DateTime.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
    public static implicit operator bool?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (double.TryParse(@this._string, out var b)) return b > 0;
        if (bool.TryParse(@this._string, out var ret)) return ret;
        return default;
    }
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET451_OR_GREATER
    public static implicit operator Guid?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (Guid.TryParse(@this._string, out var ret)) return ret;
        else return default;
    }
#else
    public static implicit operator Guid?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        try { return new Guid(@this._string); }
        catch { return default; }
    }
#endif
#if NET6_0_OR_GREATER
    public static implicit operator DateOnly?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (DateOnly.TryParse(@this._string, out var date)) return date;
        if (DateTime.TryParse(@this._string, out var dt)) return DateOnly.FromDateTime(dt);
        return default;
    }
    public static implicit operator TimeOnly?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return default;
        if (TimeOnly.TryParse(@this._string, out var date)) return date;
        if (DateTime.TryParse(@this._string, out var dt)) return TimeOnly.FromDateTime(dt);
        return default;
    }
#endif

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj is Variant variant) return ToString() == variant.ToString();

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

    public static bool operator ==(Variant left, Variant right) => left.ToString() == right.ToString();
    public static bool operator !=(Variant left, Variant right) => left.ToString() != right.ToString();

    public override string? ToString() => _string;
}
