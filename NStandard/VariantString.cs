using System;

namespace NStandard
{
    public class VariantString
    {
        public string String;

        public VariantString(string str) => String = str;

        public VariantString(char obj) => String = obj.ToString();
        public VariantString(byte obj) => String = obj.ToString();
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

        public VariantString(char? obj) => String = obj?.ToString();
        public VariantString(byte? obj) => String = obj?.ToString();
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

        public static implicit operator VariantString(string str) => new VariantString(str);
        public static implicit operator string(VariantString @this) => @this.String;

        public static implicit operator VariantString(char obj) => new VariantString(obj);
        public static implicit operator VariantString(byte obj) => new VariantString(obj);
        public static implicit operator VariantString(short obj) => new VariantString(obj);
        public static implicit operator VariantString(ushort obj) => new VariantString(obj);
        public static implicit operator VariantString(int obj) => new VariantString(obj);
        public static implicit operator VariantString(uint obj) => new VariantString(obj);
        public static implicit operator VariantString(long obj) => new VariantString(obj);
        public static implicit operator VariantString(ulong obj) => new VariantString(obj);
        public static implicit operator VariantString(float obj) => new VariantString(obj);
        public static implicit operator VariantString(double obj) => new VariantString(obj);
        public static implicit operator VariantString(decimal obj) => new VariantString(obj);
        public static implicit operator VariantString(DateTime obj) => new VariantString(obj);
        public static implicit operator VariantString(bool obj) => new VariantString(obj);
        public static implicit operator VariantString(Guid obj) => new VariantString(obj);

        public static implicit operator VariantString(char? obj) => new VariantString(obj);
        public static implicit operator VariantString(byte? obj) => new VariantString(obj);
        public static implicit operator VariantString(short? obj) => new VariantString(obj);
        public static implicit operator VariantString(ushort? obj) => new VariantString(obj);
        public static implicit operator VariantString(int? obj) => new VariantString(obj);
        public static implicit operator VariantString(uint? obj) => new VariantString(obj);
        public static implicit operator VariantString(long? obj) => new VariantString(obj);
        public static implicit operator VariantString(ulong? obj) => new VariantString(obj);
        public static implicit operator VariantString(float? obj) => new VariantString(obj);
        public static implicit operator VariantString(double? obj) => new VariantString(obj);
        public static implicit operator VariantString(decimal? obj) => new VariantString(obj);
        public static implicit operator VariantString(DateTime? obj) => new VariantString(obj);
        public static implicit operator VariantString(bool? obj) => new VariantString(obj);
        public static implicit operator VariantString(Guid? obj) => new VariantString(obj);

        public static implicit operator char(VariantString @this) => char.TryParse(@this.String, out var ret).For(x => x ? ret : default);
        public static implicit operator byte(VariantString @this) => byte.TryParse(@this.String, out var ret).For(x => x ? ret : default);
        public static implicit operator short(VariantString @this) => short.TryParse(@this.String, out var ret).For(x => x ? ret : default);
        public static implicit operator ushort(VariantString @this) => ushort.TryParse(@this.String, out var ret).For(x => x ? ret : default);
        public static implicit operator int(VariantString @this) => int.TryParse(@this.String, out var ret).For(x => x ? ret : default);
        public static implicit operator uint(VariantString @this) => uint.TryParse(@this.String, out var ret).For(x => x ? ret : default);
        public static implicit operator long(VariantString @this) => long.TryParse(@this.String, out var ret).For(x => x ? ret : default);
        public static implicit operator ulong(VariantString @this) => ulong.TryParse(@this.String, out var ret).For(x => x ? ret : default);
        public static implicit operator float(VariantString @this) => float.TryParse(@this.String, out var ret).For(x => x ? ret : default);
        public static implicit operator double(VariantString @this) => double.TryParse(@this.String, out var ret).For(x => x ? ret : default);
        public static implicit operator decimal(VariantString @this) => decimal.TryParse(@this.String, out var ret).For(x => x ? ret : default);
        public static implicit operator DateTime(VariantString @this) => DateTime.TryParse(@this.String, out var ret).For(x => x ? ret : default);
        public static implicit operator bool(VariantString @this)
        {
            if (@this.String.IsNullOrEmpty()) return false;
            if (double.TryParse(@this.String, out var b)) return b > 0;
            return bool.TryParse(@this.String, out var ret).For(x => x ? ret : default);
        }
#if NET35
        public static implicit operator Guid(VariantString @this)
        {
            try { return new Guid(@this.String); }
            catch { return Guid.Empty; }
        }
#else
        public static implicit operator Guid(VariantString @this) => Guid.TryParse(@this.String, out var ret).For(x => x ? ret : default);
#endif

        public static implicit operator char?(VariantString @this) => char.TryParse(@this.String, out var ret).For(x => x ? ret : (char?)null);
        public static implicit operator byte?(VariantString @this) => byte.TryParse(@this.String, out var ret).For(x => x ? ret : (byte?)null);
        public static implicit operator short?(VariantString @this) => short.TryParse(@this.String, out var ret).For(x => x ? ret : (short?)null);
        public static implicit operator ushort?(VariantString @this) => ushort.TryParse(@this.String, out var ret).For(x => x ? ret : (ushort?)null);
        public static implicit operator int?(VariantString @this) => int.TryParse(@this.String, out var ret).For(x => x ? ret : (int?)null);
        public static implicit operator uint?(VariantString @this) => uint.TryParse(@this.String, out var ret).For(x => x ? ret : (uint?)null);
        public static implicit operator long?(VariantString @this) => long.TryParse(@this.String, out var ret).For(x => x ? ret : (long?)null);
        public static implicit operator ulong?(VariantString @this) => ulong.TryParse(@this.String, out var ret).For(x => x ? ret : (ulong?)null);
        public static implicit operator float?(VariantString @this) => float.TryParse(@this.String, out var ret).For(x => x ? ret : (float?)null);
        public static implicit operator double?(VariantString @this) => double.TryParse(@this.String, out var ret).For(x => x ? ret : (double?)null);
        public static implicit operator decimal?(VariantString @this) => decimal.TryParse(@this.String, out var ret).For(x => x ? ret : (decimal?)null);
        public static implicit operator DateTime?(VariantString @this) => DateTime.TryParse(@this.String, out var ret).For(x => x ? ret : (DateTime?)default);
        public static implicit operator bool?(VariantString @this)
        {
            if (@this.String.IsNullOrEmpty()) return null;
            if (double.TryParse(@this.String, out var b)) return b > 0;
            return bool.TryParse(@this.String, out var ret).For(x => x ? ret : (bool?)null);
        }
#if NET35
        public static implicit operator Guid?(VariantString @this)
        {
            try { return new Guid(@this.String); }
            catch { return null; }
        }
#else
        public static implicit operator Guid?(VariantString @this) => Guid.TryParse(@this.String, out var ret).For(x => x ? ret : (Guid?)default);
#endif

        public override string ToString() => String?.ToString() ?? "";
    }
}
