using System;
using System.IO;

namespace NStandard.IO
{
    public class BitStream : Stream
    {
        private readonly MemoryStream _memory;
        private readonly bool _padRight;
        private readonly int _length;
        private int _bitOffset;
        private int BitRest => 8 - _bitOffset;

        public BitStream(byte[] buffer, bool padRight)
        {
            _memory = new MemoryStream(buffer);
            _padRight = padRight;
            _length = buffer.Length * 8;
        }

        public override bool CanRead => true;

        public override bool CanSeek => true;

        public override bool CanWrite => false;

        public override long Length => _length;

        public override long Position
        {
            get => _memory.Position * 8 + _bitOffset;
            set
            {
                _memory.Position = value / 8;
                _bitOffset = (int)(value % 8);
            }
        }

        public override void Flush() => throw new NotSupportedException("Bit stream is readonly.");

        private int ReadByte(int bits, ref byte outByte)
        {
            if (_memory.Position >= _memory.Length) return 0;

            var rest = BitRest;
            int _byte;

            int read;
            if (rest < bits)
            {
                _byte = (_memory.ReadByte() << _bitOffset) & 0xff;

                if (_memory.Position < _memory.Length)
                {
                    var next = _memory.ReadByte();
                    _memory.Position--;

                    _byte |= next >> rest;
                    _bitOffset = bits - rest;
                    read = bits;
                }
                else
                {
                    _bitOffset = 0;
                    read = rest;
                }
            }
            else
            {
                _byte = (_memory.ReadByte() << _bitOffset) & 0xff;
                _bitOffset += bits;
                read = bits;

                if (_bitOffset < 8)
                {
                    _memory.Position--;
                }
                else
                {
                    _bitOffset = 0;
                }
            }

            var clear = 8 - bits;
            _byte >>= clear;

            outByte = _padRight ? (byte)(_byte << clear) : (byte)_byte;
            return read;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var read = 0;
            for (int i = offset; count > 0; i++)
            {
                if (count < 8)
                {
                    read += ReadByte(count, ref buffer[i]);
                    break;
                }
                else
                {
                    read += ReadByte(8, ref buffer[i]);
                }
            }
            return read;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            var _position = origin switch
            {
                SeekOrigin.Begin => offset,
                SeekOrigin.Current => Position + offset,
                SeekOrigin.End => _length + offset,
                _ => throw new NotImplementedException(),
            };

            Position = _position;
            return _position;
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException("Bit stream is not expandable.");
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Bit stream is readonly.");
        }
    }
}
