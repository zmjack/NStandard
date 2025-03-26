using System.Security.Cryptography;

namespace NStandard.Security;

public class RsaProvider
{
    private readonly RSACryptoServiceProvider _innerProvider = new();

    public void FromXmlString(string xmlString)
    {
        _innerProvider.FromXmlString(xmlString);
        /*
        var @params = new RSAParameters();
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlString);
        foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
        {
            switch (node.Name)
            {
                case "Modulus": @params.Modulus = Convert.FromBase64String(node.InnerText); break;
                case "Exponent": @params.Exponent = Convert.FromBase64String(node.InnerText); break;
                case "P": @params.P = Convert.FromBase64String(node.InnerText); break;
                case "Q": @params.Q = Convert.FromBase64String(node.InnerText); break;
                case "DP": @params.DP = Convert.FromBase64String(node.InnerText); break;
                case "DQ": @params.DQ = Convert.FromBase64String(node.InnerText); break;
                case "InverseQ": @params.InverseQ = Convert.FromBase64String(node.InnerText); break;
                case "D": @params.D = Convert.FromBase64String(node.InnerText); break;
            }
        }
        _innerProvider.ImportParameters(@params);
        */
    }

    public void FromPemString(string pemString)
    {
        pemString = pemString.Replace("\r\n", string.Empty);

        string keyName;
        if (pemString.StartsWith("-----BEGIN PRIVATE KEY-----")) keyName = "PRIVATE";
        else if (pemString.StartsWith("-----BEGIN PUBLIC KEY-----")) keyName = "PUBLIC";
        else throw new ArgumentException("Can't analyze Pem string.");

        pemString = pemString
            .Replace($"-----BEGIN {keyName} KEY-----", string.Empty)
            .Replace($"-----END {keyName} KEY-----", string.Empty);
        var pem = Convert.FromBase64String(pemString);
        var @params = RsaConverter.ParamsFromPem(pem, keyName == "PRIVATE");

        _innerProvider.ImportParameters(@params);
    }

    public string ToXmlString(bool includePrivateParameters)
    {
        return _innerProvider.ToXmlString(includePrivateParameters);
        /*
        var @params = _innerProvider.ExportParameters(includePrivateParameters);
        if (includePrivateParameters)
        {
            return $"<RSAKeyValue>\r\n" +
                $"<Modulus>{Convert.ToBase64String(@params.Modulus)}</Modulus>\r\n" +
                $"<Exponent>{Convert.ToBase64String(@params.Exponent)}</Exponent>\r\n" +
                $"<P>{Convert.ToBase64String(@params.P)}</P>\r\n" +
                $"<Q>{Convert.ToBase64String(@params.Q)}</Q>\r\n" +
                $"<DP>{Convert.ToBase64String(@params.DP)}</DP>\r\n" +
                $"<DQ>{Convert.ToBase64String(@params.DQ)}</DQ>\r\n" +
                $"<InverseQ>{Convert.ToBase64String(@params.InverseQ)}</InverseQ>\r\n" +
                $"<D>{Convert.ToBase64String(@params.D)}</D>\r\n" +
                $"</RSAKeyValue>";
        }
        else
        {
            return $"<RSAKeyValue>\r\n" +
                $"<Modulus>{Convert.ToBase64String(@params.Modulus)}</Modulus>\r\n" +
                $"<Exponent>{Convert.ToBase64String(@params.Exponent)}</Exponent>\r\n" +
                $"</RSAKeyValue>";
        }
        */
    }

    public string ToPemString(bool includePrivateParameters)
    {
        var keyName = includePrivateParameters ? "PRIVATE" : "PUBLIC";
        var @params = _innerProvider.ExportParameters(includePrivateParameters);
        var base64 = string.Join(Environment.NewLine,
            Convert.ToBase64String(
                RsaConverter.ParamsToPem(@params, includePrivateParameters)
            )
                .ToCharArray().Pairs()
                .GroupBy(x => x.Index / 64)
                .Select(g => new string(g.Select(x => x.Value).ToArray()))
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_0_OR_GREATER || NET451_OR_GREATER
#else
                .ToArray()
#endif
            );
        return $"-----BEGIN {keyName} KEY-----\r\n{base64}\r\n-----END {keyName} KEY-----";
    }

    /// <summary>
    /// Encrypt data for Pkcs1 padding.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public byte[] Encrypt(byte[] data) => Encrypt(data, RSAEncryptionPadding.Pkcs1);
    /// <summary>
    /// Encrypt data.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="padding"></param>
    /// <returns></returns>
    public byte[] Encrypt(byte[] data, RSAEncryptionPadding padding)
    {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET46_OR_GREATER
        return _innerProvider.Encrypt(data, padding);
#else
        if (padding == RSAEncryptionPadding.Pkcs1) return _innerProvider.Encrypt(data, false);
        else if (padding == RSAEncryptionPadding.OaepSHA1) return _innerProvider.Encrypt(data, true);
        else throw new NotSupportedException("Only Pkcs1 and OaepSHA1 are supported.");
#endif
    }

    /// <summary>
    /// Decrypt data for Pkcs1.
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public byte[] Decrypt(byte[] data) => Decrypt(data, RSAEncryptionPadding.Pkcs1);
    /// <summary>
    /// Decrypt data.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="padding"></param>
    /// <returns></returns>
    public byte[] Decrypt(byte[] data, RSAEncryptionPadding padding)
    {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET46_OR_GREATER
        return _innerProvider.Decrypt(data, padding);
#else
        if (padding == RSAEncryptionPadding.Pkcs1) return _innerProvider.Decrypt(data, false);
        else if (padding == RSAEncryptionPadding.OaepSHA1) return _innerProvider.Decrypt(data, true);
        else throw new NotSupportedException("Only RSAEncryptionPadding.Pkcs1 and RSAEncryptionPadding.OaepSHA1 are supported.");
#endif
    }

#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_3_OR_GREATER || NET46_OR_GREATER
#else
    private HashAlgorithm GetHashAlgorithm(HashAlgorithmName hashAlgorithm)
    {
        return hashAlgorithm switch
        {
            HashAlgorithmName when hashAlgorithm == HashAlgorithmName.MD5 => MD5.Create(),
            HashAlgorithmName when hashAlgorithm == HashAlgorithmName.SHA1 => SHA1.Create(),
            HashAlgorithmName when hashAlgorithm == HashAlgorithmName.SHA256 => SHA256.Create(),
            HashAlgorithmName when hashAlgorithm == HashAlgorithmName.SHA384 => SHA384.Create(),
            HashAlgorithmName when hashAlgorithm == HashAlgorithmName.SHA512 => SHA512.Create(),
            _ => throw new NotSupportedException(),
        };
    }

    private string GetOid(HashAlgorithmName hashAlgorithm)
    {
        return hashAlgorithm switch
        {
            HashAlgorithmName when hashAlgorithm == HashAlgorithmName.MD5 => Oids.Md5,
            HashAlgorithmName when hashAlgorithm == HashAlgorithmName.SHA1 => Oids.Sha1,
            HashAlgorithmName when hashAlgorithm == HashAlgorithmName.SHA256 => Oids.Sha256,
            HashAlgorithmName when hashAlgorithm == HashAlgorithmName.SHA384 => Oids.Sha384,
            HashAlgorithmName when hashAlgorithm == HashAlgorithmName.SHA512 => Oids.Sha512,
            _ => throw new NotSupportedException(),
        };
    }
#endif

    public byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm) => SignData(data, hashAlgorithm, RSASignaturePadding.Pkcs1);
    public byte[] SignData(byte[] data, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
#if NETCOREAPP2_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
        return _innerProvider.SignData(data, hashAlgorithm, padding);
#else
        if (padding != RSASignaturePadding.Pkcs1) throw new NotSupportedException("Only RSASignaturePadding.Pkcs1 is supported.");
        return _innerProvider.SignData(data, GetHashAlgorithm(hashAlgorithm));
#endif
    }

    public byte[] SignData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm) => SignData(data, offset, count, hashAlgorithm, RSASignaturePadding.Pkcs1);
    public byte[] SignData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
#if NETCOREAPP2_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
        return _innerProvider.SignData(data, offset, count, hashAlgorithm, padding);
#else
        if (padding != RSASignaturePadding.Pkcs1) throw new NotSupportedException("Only RSASignaturePadding.Pkcs1 is supported.");
        return _innerProvider.SignData(data, offset, count, GetHashAlgorithm(hashAlgorithm));
#endif
    }

    public byte[] SignData(Stream data, HashAlgorithmName hashAlgorithm) => SignData(data, hashAlgorithm, RSASignaturePadding.Pkcs1);
    public byte[] SignData(Stream data, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
#if NETCOREAPP2_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
        return _innerProvider.SignData(data, hashAlgorithm, padding);
#else
        if (padding != RSASignaturePadding.Pkcs1) throw new NotSupportedException("Only RSASignaturePadding.Pkcs1 is supported.");
        return _innerProvider.SignData(data, GetHashAlgorithm(hashAlgorithm));
#endif
    }

    public bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithm) => VerifyData(data, signature, hashAlgorithm, RSASignaturePadding.Pkcs1);
    public bool VerifyData(byte[] data, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
#if NETCOREAPP2_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
        return _innerProvider.VerifyData(data, signature, hashAlgorithm, padding);
#else
        if (padding != RSASignaturePadding.Pkcs1) throw new NotSupportedException("Only RSASignaturePadding.Pkcs1 is supported.");
        return _innerProvider.VerifyData(data, GetHashAlgorithm(hashAlgorithm), signature);
#endif
    }

    public bool VerifyData(byte[] data, int offset, int count, byte[] signature, HashAlgorithmName hashAlgorithm) => VerifyData(data, offset, count, signature, hashAlgorithm, RSASignaturePadding.Pkcs1);
    public bool VerifyData(byte[] data, int offset, int count, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
#if NETCOREAPP2_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
        return _innerProvider.VerifyData(data, offset, count, signature, hashAlgorithm, padding);
#else
        if (padding != RSASignaturePadding.Pkcs1) throw new NotSupportedException("Only RSASignaturePadding.Pkcs1 is supported.");
        if (offset < 0) throw new ArgumentException("Non-negative number required.", nameof(offset));
        if (count < 0) throw new ArgumentException("Value was invalid.");
        if (offset + count > data.Length) throw new ArgumentException("Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.");

        var bytes = new byte[count];
        Array.Copy(data, offset, bytes, 0, count);
        return _innerProvider.VerifyData(bytes, GetHashAlgorithm(hashAlgorithm), signature);
#endif
    }

    public bool VerifyData(Stream data, byte[] signature, HashAlgorithmName hashAlgorithm) => VerifyData(data, signature, hashAlgorithm, RSASignaturePadding.Pkcs1);
    public bool VerifyData(Stream data, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
#if NETCOREAPP2_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
        return _innerProvider.VerifyData(data, signature, hashAlgorithm, padding);
#else
        if (padding != RSASignaturePadding.Pkcs1) throw new NotSupportedException("Only RSASignaturePadding.Pkcs1 is supported.");

        using var stream = new MemoryStream();
        data.ScanAndWriteTo(stream, 1024 * 1024);
        var bytes = stream.ToArray();
        return _innerProvider.VerifyData(bytes, GetHashAlgorithm(hashAlgorithm), signature);
#endif
    }
    public byte[] SignHash(byte[] hash, HashAlgorithmName hashAlgorithm) => SignHash(hash, hashAlgorithm, RSASignaturePadding.Pkcs1);
    public byte[] SignHash(byte[] hash, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
#if NETCOREAPP2_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
        return _innerProvider.SignHash(hash, hashAlgorithm, padding);
#else
        if (padding != RSASignaturePadding.Pkcs1) throw new NotSupportedException("Only RSASignaturePadding.Pkcs1 is supported.");
        return _innerProvider.SignHash(hash, GetOid(hashAlgorithm));
#endif
    }

    public bool VerifyHash(byte[] hash, byte[] signature, HashAlgorithmName hashAlgorithm) => VerifyHash(hash, signature, hashAlgorithm, RSASignaturePadding.Pkcs1);
    public bool VerifyHash(byte[] hash, byte[] signature, HashAlgorithmName hashAlgorithm, RSASignaturePadding padding)
    {
#if NETCOREAPP2_0_OR_GREATER || NETSTANDARD2_0_OR_GREATER || NET46_OR_GREATER
        return _innerProvider.VerifyHash(hash, signature, hashAlgorithm, padding);
#else
        if (padding != RSASignaturePadding.Pkcs1) throw new NotSupportedException("Only RSASignaturePadding.Pkcs1 is supported.");
        return _innerProvider.VerifyHash(hash, GetOid(hashAlgorithm), signature);
#endif
    }

}
