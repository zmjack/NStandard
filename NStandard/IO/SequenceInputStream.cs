using System;
using System.IO;
using System.Linq;

namespace NStandard.IO
{
    public class SequenceInputStream : SequenceInputStream<Stream>
    {
        public static SequenceInputStream<TStream> Create<TStream>(params TStream[] streams)
            where TStream : Stream => new(streams);

        public SequenceInputStream(params Stream[] streams) : base(streams) { }
    }

    public class SequenceInputStream<TStream> : Stream
        where TStream : Stream
    {
        public Stream[] Streams { get; private set; }
        private readonly long[] _partialLengths;
        private readonly int _partialCount;
        private readonly long _length;
        private long _position;
        private int _selectedIndex;
        public long _selectedPosition;

        public override bool CanRead => true;
        public override bool CanSeek => true;
        public override bool CanWrite => false;
        public override long Length => _length;

        public override long Position
        {
            get => _position;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("The parameter `position` can not be less than 0.");

                long beforeLength = 0;
                for (int i = 0; i < _partialCount; i++)
                {
                    if (value < beforeLength + _partialLengths[i])
                    {
                        _position = value;
                        _selectedIndex = i;
                        _selectedPosition = value - beforeLength;
                        return;
                    }
                    else beforeLength += _partialLengths[i];
                }

                _position = value;
                _selectedIndex = _partialCount;
                _selectedPosition = 0;
            }
        }

        public SequenceInputStream(params TStream[] streams)
        {
            Streams = streams;
            _partialLengths = streams.Select(x => x.Length).ToArray();
            _partialCount = _partialLengths.Length;
            _length = streams.Sum(x => x.Length);
        }

        public override void Flush() => throw new NotSupportedException();
        public override long Seek(long offset, SeekOrigin origin)
        {
            switch (origin)
            {
                case SeekOrigin.Begin: Position = offset; break;
                case SeekOrigin.Current: Position += offset; break;
                case SeekOrigin.End: Position = _length - 1 + offset; break;
            }

            return _position;
        }
        public override void SetLength(long value) => throw new NotSupportedException();

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer is null)
                throw new ArgumentNullException(nameof(buffer), "The parameter `buffer` can not be null.");
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), "The parameter `offset` can not be less than 0.");
            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), "The parameter `count` can not be less than 0.");
            if (buffer.Length - offset < count)
                throw new ArgumentException("Invalid length.");

            var read = 0;
            for (int needRead = count; needRead > 0;)
            {
                if (_selectedIndex >= _partialCount) break;

                var stream = Streams[_selectedIndex];
                var streamRemainingBytes = stream.Length - _selectedPosition;
                stream.Position = _selectedPosition;

                if (needRead < streamRemainingBytes)
                {
                    stream.Read(buffer, offset, needRead);
                    read += needRead;
                    Position += needRead;
                    break;
                }
                else
                {
                    if (needRead == streamRemainingBytes)
                    {
                        stream.Read(buffer, offset, needRead);
                        read += needRead;
                        Position += needRead;
                        break;
                    }
                    else
                    {
                        stream.Read(buffer, offset, (int)streamRemainingBytes);
                        read += (int)streamRemainingBytes;
                        Position += (int)streamRemainingBytes;

                        offset += (int)streamRemainingBytes;
                        needRead -= (int)streamRemainingBytes;
                    }
                }
            }

            return read;
        }

        public override void Write(byte[] buffer, int offset, int count) => throw new NotSupportedException();
    }

}
