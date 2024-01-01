using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace NStandard.Security;

internal class RsaConverter
{
    public static byte[] ParamsToPem(RSAParameters parameters, bool includePrivateParameters)
    {
        if (includePrivateParameters)
        {
            return new PemNode(PemNode.DataType.Sequence, new[]
            {
                new PemNode(PemNode.DataType.Integer, new byte[] { 0x00 }),
                new PemNode(PemNode.DataType.Sequence, PemNode.RSA_OID),
                new PemNode(PemNode.DataType.OctetString, new[]
                {
                    new PemNode(PemNode.DataType.Sequence, new[]
                    {
                        new PemNode(PemNode.DataType.Integer, new byte[] { 0x00 } ),
                        new PemNode(PemNode.DataType.Integer, parameters.Modulus ),
                        new PemNode(PemNode.DataType.Integer, parameters.Exponent ),
                        new PemNode(PemNode.DataType.Integer, parameters.D ),
                        new PemNode(PemNode.DataType.Integer, parameters.P ),
                        new PemNode(PemNode.DataType.Integer, parameters.Q ),
                        new PemNode(PemNode.DataType.Integer, parameters.DP ),
                        new PemNode(PemNode.DataType.Integer, parameters.DQ ),
                        new PemNode(PemNode.DataType.Integer, parameters.InverseQ ),
                    })
                }),
            }).Fragment;
        }
        else
        {
            return new PemNode(PemNode.DataType.Sequence, new[]
            {
                new PemNode(PemNode.DataType.Sequence, PemNode.RSA_OID),
                new PemNode(PemNode.DataType.BitString, new[]
                {
                    new PemNode(PemNode.DataType.Sequence, new[]
                    {
                        new PemNode(PemNode.DataType.Integer, parameters.Modulus ),
                        new PemNode(PemNode.DataType.Integer, parameters.Exponent ),
                    })
                }),
            }).Fragment;
        }
    }

    public static RSAParameters ParamsFromPem(byte[] pem, bool includePrivateParameters)
    {
        byte[][] resolved;
        if (includePrivateParameters)
        {
            resolved = Resolve(Resolve(Resolve(Resolve(pem,
            [
                PemNode.DataType.Sequence,
            ])[0],
            [
                PemNode.DataType.Integer,
                PemNode.DataType.Sequence,
                PemNode.DataType.OctetString,
            ])[2],
            [
                PemNode.DataType.Sequence,
            ])[0], [
                PemNode.DataType.Integer,
                PemNode.DataType.Integer,
                PemNode.DataType.Integer,
                PemNode.DataType.Integer,
                PemNode.DataType.Integer,
                PemNode.DataType.Integer,
                PemNode.DataType.Integer,
                PemNode.DataType.Integer,
                PemNode.DataType.Integer,
            ]);

            return new RSAParameters
            {
                Modulus = resolved[1],
                Exponent = resolved[2],
                D = resolved[3],
                P = resolved[4],
                Q = resolved[5],
                DP = resolved[6],
                DQ = resolved[7],
                InverseQ = resolved[8],
            };
        }
        else
        {
            resolved = Resolve(Resolve(Resolve(Resolve(pem,
            [
                PemNode.DataType.Sequence,
            ])[0],
            [
                PemNode.DataType.Sequence,
                PemNode.DataType.BitString,
            ])[1],
            [
                PemNode.DataType.Sequence,
            ])[0], [
                PemNode.DataType.Integer,
                PemNode.DataType.Integer,
            ]);

            return new RSAParameters
            {
                Modulus = resolved[0],
                Exponent = resolved[1],
            };
        }
    }

    private static byte[][] Resolve(byte[] fragment, PemNode.DataType[] dataTypes)
    {
        var bytesList = new List<byte[]>();
        using (var memory = new MemoryStream(fragment))
        {
            foreach (var dataType in dataTypes)
            {
                var type = (PemNode.DataType)memory.ReadByte();
                if (type != dataType)
                    throw new ArgumentException($"Argument[{bytesList.Count}] is not a '{dataType}'.");

                var lengthBytes = new byte[5];
                lengthBytes[0] = (byte)memory.ReadByte();

                switch (lengthBytes[0])
                {
                    case byte sign when sign == 0x81: memory.Read(lengthBytes, 1, 1); break;
                    case byte sign when sign == 0x82: memory.Read(lengthBytes, 1, 2); break;
                    case byte sign when sign == 0x83: memory.Read(lengthBytes, 1, 3); break;
                    case byte sign when sign == 0x84: memory.Read(lengthBytes, 1, 4); break;
                }
                var length = PemNode.GetLength(lengthBytes);

                byte[] data;
                var first = (byte)memory.ReadByte();
                if (first == 0)
                {
                    data = new byte[length - 1];
                    memory.Read(data, 0, data.Length);
                }
                else
                {
                    data = new byte[length];
                    data[0] = first;
                    memory.Read(data, 1, data.Length - 1);
                }

                bytesList.Add(data);
            }
        }

        return bytesList.ToArray();
    }

}
