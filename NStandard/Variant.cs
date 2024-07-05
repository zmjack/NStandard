using System;

namespace NStandard;

public class Variant
{
    private static InvalidCastException TypeMismatch() => new("Type mismatch.");

    private readonly string _string;

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

    public Variant(char? obj) => _string = obj?.ToString() ?? string.Empty;
    public Variant(byte? obj) => _string = obj?.ToString() ?? string.Empty;
    public Variant(sbyte? obj) => _string = obj?.ToString() ?? string.Empty;
    public Variant(short? obj) => _string = obj?.ToString() ?? string.Empty;
    public Variant(ushort? obj) => _string = obj?.ToString() ?? string.Empty;
    public Variant(int? obj) => _string = obj?.ToString() ?? string.Empty;
    public Variant(uint? obj) => _string = obj?.ToString() ?? string.Empty;
    public Variant(long? obj) => _string = obj?.ToString() ?? string.Empty;
    public Variant(ulong? obj) => _string = obj?.ToString() ?? string.Empty;
    public Variant(float? obj) => _string = obj?.ToString() ?? string.Empty;
    public Variant(double? obj) => _string = obj?.ToString() ?? string.Empty;
    public Variant(decimal? obj) => _string = obj?.ToString() ?? string.Empty;
    public Variant(DateTime? obj) => _string = obj?.ToString() ?? string.Empty;
    public Variant(bool? obj) => _string = obj?.ToString() ?? string.Empty;
    public Variant(Guid? obj) => _string = obj?.ToString() ?? string.Empty;
#if NET6_0_OR_GREATER
    public Variant(DateOnly? obj) => _string = obj?.ToString() ?? string.Empty;
    public Variant(TimeOnly? obj) => _string = obj?.ToString() ?? string.Empty;
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
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (char.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator byte(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (byte.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator sbyte(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (sbyte.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator short(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (short.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator ushort(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (ushort.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator int(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (int.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator uint(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (uint.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator long(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (long.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator ulong(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (ulong.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator float(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (float.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator double(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (double.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator decimal(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (decimal.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator DateTime(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (DateTime.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator bool(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (double.TryParse(@this._string, out var b)) return b > 0;
        if (bool.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET451_OR_GREATER
    public static implicit operator Guid(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (Guid.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
#else
    public static implicit operator Guid(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        try { return new Guid(@this._string); }
        catch { throw TypeMismatch(); }
    }
#endif
#if NET6_0_OR_GREATER
    public static implicit operator DateOnly(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (DateOnly.TryParse(@this._string, out var date)) return date;
        if (DateTime.TryParse(@this._string, out var dt)) return DateOnly.FromDateTime(dt);
        throw TypeMismatch();
    }
    public static implicit operator TimeOnly(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) throw TypeMismatch();
        if (TimeOnly.TryParse(@this._string, out var date)) return date;
        if (DateTime.TryParse(@this._string, out var dt)) return TimeOnly.FromDateTime(dt);
        throw TypeMismatch();
    }
#endif

    public static implicit operator char?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (char.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator byte?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (byte.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator sbyte?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (sbyte.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator short?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (short.TryParse(@this._string, out var ret)) return ret;
        return null;
    }
    public static implicit operator ushort?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (ushort.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator int?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (int.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator uint?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (uint.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator long?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (long.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator ulong?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (ulong.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator float?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (float.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator double?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (double.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator decimal?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (decimal.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator DateTime?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (DateTime.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
    public static implicit operator bool?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (double.TryParse(@this._string, out var b)) return b > 0;
        if (bool.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET451_OR_GREATER
    public static implicit operator Guid?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (Guid.TryParse(@this._string, out var ret)) return ret;
        throw TypeMismatch();
    }
#else
    public static implicit operator Guid?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        try { return new Guid(@this._string); }
        catch { throw TypeMismatch(); }
    }
#endif
#if NET6_0_OR_GREATER
    public static implicit operator DateOnly?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (DateOnly.TryParse(@this._string, out var date)) return date;
        if (DateTime.TryParse(@this._string, out var dt)) return DateOnly.FromDateTime(dt);
        throw TypeMismatch();
    }
    public static implicit operator TimeOnly?(Variant @this)
    {
        if (@this._string.IsNullOrWhiteSpace()) return null;
        if (TimeOnly.TryParse(@this._string, out var date)) return date;
        if (DateTime.TryParse(@this._string, out var dt)) return TimeOnly.FromDateTime(dt);
        throw TypeMismatch();
    }
#endif

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj is Variant variant) return _string == variant._string;

        switch (obj)
        {
            case char o: return _string == o.ToString();
            case byte o: return _string == o.ToString();
            case sbyte o: return _string == o.ToString();
            case short o: return _string == o.ToString();
            case ushort o: return _string == o.ToString();
            case int o: return _string == o.ToString();
            case uint o: return _string == o.ToString();
            case long o: return _string == o.ToString();
            case ulong o: return _string == o.ToString();
            case float o: return _string == o.ToString();
            case double o: return _string == o.ToString();
            case decimal o: return _string == o.ToString();
            case DateTime o: return _string == o.ToString();
            case bool o: return _string == o.ToString();
            case Guid o: return _string == o.ToString();
#if NET6_0_OR_GREATER
            case DateOnly o: return _string == o.ToString();
            case TimeOnly o: return _string == o.ToString();
#endif
        };

        switch (obj.GetType())
        {
            case Type type when type == typeof(char?): return _string == obj?.ToString();
            case Type type when type == typeof(byte?): return _string == obj?.ToString();
            case Type type when type == typeof(sbyte?): return _string == obj?.ToString();
            case Type type when type == typeof(short?): return _string == obj?.ToString();
            case Type type when type == typeof(ushort?): return _string == obj?.ToString();
            case Type type when type == typeof(int?): return _string == obj?.ToString();
            case Type type when type == typeof(uint?): return _string == obj?.ToString();
            case Type type when type == typeof(long?): return _string == obj?.ToString();
            case Type type when type == typeof(ulong?): return _string == obj?.ToString();
            case Type type when type == typeof(float?): return _string == obj?.ToString();
            case Type type when type == typeof(double?): return _string == obj?.ToString();
            case Type type when type == typeof(decimal?): return _string == obj?.ToString();
            case Type type when type == typeof(DateTime?): return _string == obj?.ToString();
            case Type type when type == typeof(bool?): return _string == obj?.ToString();
            case Type type when type == typeof(Guid?): return _string == obj?.ToString();
#if NET6_0_OR_GREATER
            case Type type when type == typeof(DateOnly?): return _string == obj?.ToString();
            case Type type when type == typeof(TimeOnly?): return _string == obj?.ToString();
#endif
        }

        return false;
    }

    public static bool operator ==(Variant left, Variant right) => left._string == right._string;
    public static bool operator !=(Variant left, Variant right) => left._string != right._string;

    public override string ToString() => _string;
}
