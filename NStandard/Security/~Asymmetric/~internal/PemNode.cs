using System;
using System.Collections.Generic;

namespace NStandard.Security;

internal class PemNode
{
    public enum DataType
    {
        Integer = 0x02,
        BitString = 0x03,
        OctetString = 0x04,
        Sequence = 0x30,
    }

    public static readonly byte[] RSA_OID = [0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00];

    public DataType Type;
    public byte[]? Data { get; private set; }
    public PemNode[]? Nodes { get; private set; }
    public bool IsContainerNode { get; private set; }

    public byte[] Fragment
    {
        get
        {
            var fragment = new List<byte>();
            var data = new List<byte>();

            if (Data is not null)
            {
                if (Data[0] >= 0x80)
                {
                    data.Add(0);
                }
                data.AddRange(Data);
            }
            else
            {
                if (Type == DataType.BitString)
                {
                    data.Add(0);
                }

                foreach (var node in Nodes!)
                {
                    data.AddRange(node.Fragment);
                }
            }

            fragment.Add((byte)Type);
            fragment.AddRange(GetLengthData(data.Count));
            fragment.AddRange(data);

            return [.. fragment];
        }
    }

    public PemNode(DataType type, byte[] data)
    {
        Type = type;
        Data = data;
    }

    public PemNode(DataType type, PemNode[] nodes)
    {
        Type = type;
        Nodes = nodes;
    }

    public static int GetLength(byte[] bytes)
    {
        switch (bytes[0])
        {
            case byte sign when sign < 0x80: return sign;
            case byte sign when sign == 0x81: return BitConverter.ToInt32([bytes[1], (byte)0, (byte)0, (byte)0], 0);
            case byte sign when sign == 0x82: return BitConverter.ToInt32([bytes[2], bytes[1], (byte)0, (byte)0], 0);
            case byte sign when sign == 0x83: return BitConverter.ToInt32([bytes[3], bytes[2], bytes[1], (byte)0], 0);
            case byte sign when sign == 0x84:
                if (bytes[1] <= 0x7F) return BitConverter.ToInt32([bytes[4], bytes[3], bytes[2], bytes[1]], 0);
                else goto default;
            default: throw new ArgumentException("The length cannot greater than 0x7FFFFFFF.");
        }
    }

    public static byte[] GetLengthData(int length)
    {
        return length switch
        {
            int len when len < 0x80 => [Convert.ToByte(len)],
            int len when len <= 0xFF => [0x81, Convert.ToByte(len)],
            int len when len <= 0xFFFF =>
            [
                0x82,
                Convert.ToByte((len & (0xFF00)) >> 8),
                Convert.ToByte(len & (0x00FF)),
            ],
            int len when len <= 0xFFFFFF =>
            [
                0x83,
                Convert.ToByte((len & (0xFF0000)) >> 16),
                Convert.ToByte((len & (0x00FF00)) >> 8),
                Convert.ToByte(len & (0x0000FF)),
            ],
            int len when len <= 0x7FFFFFFF =>
            [
                0x84,
                Convert.ToByte((len & (0xFF000000)) >> 24),
                Convert.ToByte((len & (0x00FF0000)) >> 16),
                Convert.ToByte((len & (0x0000FF00)) >> 8),
                Convert.ToByte(len & (0x000000FF)),
            ],
            _ => throw new ArgumentException("The length cannot be greater than 0x7FFFFFFF."),
        };
    }

}
