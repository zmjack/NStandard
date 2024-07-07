using NStandard.Text.Json;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;
using System;
using System.Text.RegularExpressions;
#if NET6_0_OR_GREATER
using System.Text.Json;
#elif NET5_0_OR_GREATER
using System.Buffers;
using System.Text.Json;
#endif

namespace NStandard.Drawing;

[DebuggerDisplay("R:{R} G:{G} B:{B} A:{A}")]
[StructLayout(LayoutKind.Explicit)]
[JsonValue<RgbaColor>]
public partial struct RgbaColor : IRgbaColor, IJsonValue
{
    private static ArgumentException InvalidColor(string str, string paramName) => new($"The string is invalid color. (RawValue: {str})", paramName);

    [FieldOffset(3)] private byte _alpha;
    [FieldOffset(2)] private byte _red;
    [FieldOffset(1)] private byte _green;
    [FieldOffset(0)] private byte _blue;
    [FieldOffset(0)] private readonly uint _value;

    public byte R { get => _red; set => _red = value; }
    public byte G { get => _green; set => _green = value; }
    public byte B { get => _blue; set => _blue = value; }
    public byte A { get => _alpha; set => _alpha = value; }
    public uint Value => _value;

#if NET7_0_OR_GREATER
    [GeneratedRegex(@"\s*((?<n>rgb)\((?<c>.+?)\)|(?<n>rgba)\((?<c>.+?)\))\s*")]
    private static partial Regex WebColorRegex();
    private static readonly Regex _webColorRegex = WebColorRegex();
#else
    private static readonly Regex _webColorRegex = new(@"\s*((?<n>rgb|rgba)\((?<c>.+?)\))\s*");
#endif

    object? IJsonValue.Value
    {
        get
        {
            if (A == byte.MaxValue)
            {
                return $"rgb({R},{G},{B})";
            }
            else
            {
                return $"rgba({R},{G},{B},{(double)(A * 10000 / 255) / 10000})";
            }
        }
    }

#if NET5_0_OR_GREATER
    public JsonElement RawValue
    {
        set
        {
#if NET6_0_OR_GREATER
            var str = value.Deserialize<string>()!;
#else
            var bufferWriter = new ArrayBufferWriter<byte>();
            using (var writer = new Utf8JsonWriter(bufferWriter))
            {
                value.WriteTo(writer);
            }
            var str = JsonSerializer.Deserialize<string>(bufferWriter.WrittenSpan)!;
#endif
            if (str is null)
            {
                _red = 0;
                _green = 0;
                _blue = 0;
                _alpha = byte.MaxValue;
            }
            else
            {
                var match = _webColorRegex.Match(str);
                if (!match.Success) throw InvalidColor(str, nameof(RawValue));

                var groups = match.Groups;
                var name = groups["n"].Value;
                var color = groups["c"].Value.Unique()!;

                var separator = color switch
                {
                    string when color.Contains(' ') => ' ',
                    string when color.Contains(',') => ',',
                    _ => throw InvalidColor(str, nameof(RawValue)),
                };

                var parts = color.Unique()!.Split(separator);

                if (name == "rgb")
                {
                    if (parts.Length != 3) throw InvalidColor(str, nameof(RawValue));
                    _red = byte.TryParse(parts[0], out var r) ? r : throw InvalidColor(str, nameof(RawValue));
                    _green = byte.TryParse(parts[1], out var g) ? g : throw InvalidColor(str, nameof(RawValue));
                    _blue = byte.TryParse(parts[2], out var b) ? b : throw InvalidColor(str, nameof(RawValue));
                    _alpha = byte.MaxValue;
                }
                else if (name == "rgba")
                {
                    if (parts.Length != 4) throw InvalidColor(str, nameof(RawValue));

                    _red = byte.TryParse(parts[0], out var r) ? r : throw InvalidColor(str, nameof(RawValue));
                    _green = byte.TryParse(parts[1], out var g) ? g : throw InvalidColor(str, nameof(RawValue));
                    _blue = byte.TryParse(parts[2], out var b) ? b : throw InvalidColor(str, nameof(RawValue));

                    var a = parts[3] switch
                    {
                        string s when s.EndsWith('%') => double.TryParse(s.Substring(0, s.Length - 1), out var _a)
                            ? _a / 100
                            : throw InvalidColor(str, nameof(RawValue)),
                        string s => double.TryParse(s, out var _a) ? _a : throw InvalidColor(str, nameof(RawValue)),
                        _ => throw new NotImplementedException(),
                    };
                    _alpha = (byte)Math.Clamp(Math.Round(a * 255, 0), 0, 255);
                }
                else throw new NotImplementedException();
            }
        }
    }
#endif

    public RgbaColor()
    {
        _alpha = byte.MaxValue;
    }

    public RgbaColor(uint rgb, byte alpha = byte.MaxValue)
    {
        _value = rgb & 0x00ffffff;
        _alpha = alpha;
    }

    public RgbaColor(byte red, byte green, byte blue, byte alpha = byte.MaxValue)
    {
        _red = red;
        _green = green;
        _blue = blue;
        _alpha = alpha;
    }

    public RgbaColor(Color color) : this(color.R, color.G, color.B, color.A)
    {
    }

    public static readonly RgbaColor Transparent = new(0x00000000);

    public static bool operator ==(RgbaColor left, RgbaColor right) => left.Value == right.Value;
    public static bool operator !=(RgbaColor left, RgbaColor right) => left.Value != right.Value;

    public override int GetHashCode() => (int)_value;

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (obj is IRgbaColor other) return Value == other.Value;
        return false;
    }

    public static implicit operator RgbaColor(int value) => new((uint)value);
    public static implicit operator RgbaColor(uint value) => new(value);
    public static implicit operator RgbaColor(Color value) => new(value);
    public static implicit operator Color(RgbaColor value) => Color.FromArgb(value.A, value.R, value.G, value.B);

}
