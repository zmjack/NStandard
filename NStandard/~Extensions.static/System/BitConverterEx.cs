using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace NStandard
{
#if NET7_0_OR_GREATER
    public static class BitConverterEx
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int128 ToInt128(ReadOnlySpan<byte> value)
        {
            if (value.Length < 16)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            return Unsafe.ReadUnaligned<Int128>(ref MemoryMarshal.GetReference(value));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt128 ToUInt128(ReadOnlySpan<byte> value)
        {
            if (value.Length < 16)
            {
                throw new ArgumentOutOfRangeException(nameof(value));
            }
            return Unsafe.ReadUnaligned<UInt128>(ref MemoryMarshal.GetReference(value));
        }

        public static Int128 ToInt128(this byte[] value, int startIndex)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if ((uint)startIndex >= (uint)value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex), ExceptionResource.ArgumentOutOfRange_IndexMustBeLess.ToResourceString());
            }
            if (startIndex > value.Length - 16)
            {
                throw new ArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall.ToResourceString(), nameof(value));
            }

            return Unsafe.ReadUnaligned<Int128>(ref value[startIndex]);
        }

        public static UInt128 ToUInt128(this byte[] value, int startIndex)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
            if ((uint)startIndex >= (uint)value.Length)
            {
                throw new ArgumentOutOfRangeException(nameof(startIndex), ExceptionResource.ArgumentOutOfRange_IndexMustBeLess.ToResourceString());
            }
            if (startIndex > value.Length - 16)
            {
                throw new ArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall.ToResourceString(), nameof(value));
            }

            return Unsafe.ReadUnaligned<UInt128>(ref value[startIndex]);
        }

        public static byte[] GetBytes(Int128 value)
        {
            byte[] array = new byte[16];
            Unsafe.As<byte, Int128>(ref array[0]) = value;
            return array;
        }

        public static byte[] GetBytes(UInt128 value)
        {
            byte[] array = new byte[16];
            Unsafe.As<byte, UInt128>(ref array[0]) = value;
            return array;
        }

        public static bool TryWriteBytes(Span<byte> destination, Int128 value)
        {
            if (destination.Length < 16)
            {
                return false;
            }
            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }

        public static bool TryWriteBytes(Span<byte> destination, UInt128 value)
        {
            if (destination.Length < 16)
            {
                return false;
            }
            Unsafe.WriteUnaligned(ref MemoryMarshal.GetReference(destination), value);
            return true;
        }
    }
#endif
}
