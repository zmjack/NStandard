using System.IO;
using System.Security.Cryptography;

namespace NStandard.Security;

public abstract class SymmetricProvider<TSymmetricCipher> where TSymmetricCipher : SymmetricCipher<TSymmetricCipher>, new()
{
    public abstract byte[] EmptyIV { get; }
    public int BufferSize { get; protected set; } = 1024 * 1024;
    public CipherMode Mode { get; protected set; }
    public PaddingMode Padding { get; protected set; }
    public byte[] Key { get; protected set; }

    public SymmetricProvider(CipherMode mode, PaddingMode padding)
    {
        var alg = CreateAlgorithm();
        alg.GenerateKey();
        Key = alg.Key.Clone() as byte[];
        Mode = mode;
        Padding = padding;
    }

    public SymmetricProvider(byte[] key, CipherMode mode, PaddingMode padding)
    {
        Key = key;
        Mode = mode;
        Padding = padding;
    }

    protected abstract SymmetricAlgorithm CreateAlgorithm();
    private SymmetricAlgorithm InnerCreateAlgorithm()
    {
        var alg = CreateAlgorithm();
        alg.Mode = Mode;
        alg.Padding = Padding;
        alg.Key = Key.Clone() as byte[];
        return alg;
    }

    public TSymmetricCipher Encrypt(byte[] data)
    {
        using var stream = new MemoryStream(data);
        using var cipherStream = new MemoryStream();
        Encrypt(stream, cipherStream, out var iv);

        var pair = new TSymmetricCipher
        {
            Cipher = cipherStream.ToArray(),
            IV = iv,
        };
        return pair;
    }
    public void Encrypt(Stream stream, Stream cipherStream, out byte[] iv)
    {
        var alg = InnerCreateAlgorithm();
        if (Mode == CipherMode.ECB) iv = EmptyIV;
        else
        {
            alg.GenerateIV();
            iv = alg.IV.Clone() as byte[];
        }
        using var encryptor = alg.CreateEncryptor();
        using var crypto = new CryptoStream(cipherStream, encryptor, CryptoStreamMode.Write);
        stream.ScanAndWriteTo(crypto, BufferSize);
    }

    public byte[] Decrypt(TSymmetricCipher pair)
    {
        using var cipherStream = new MemoryStream(pair.Cipher);
        using var stream = new MemoryStream();
        Decrypt(cipherStream, stream, pair.IV);
        return stream.ToArray();
    }
    public void Decrypt(Stream cipherStream, Stream Stream, byte[] iv)
    {
        var alg = InnerCreateAlgorithm();
        if (Mode == CipherMode.ECB) alg.IV = EmptyIV;
        else alg.IV = iv.Clone() as byte[];

        using var decryptor = alg.CreateDecryptor(alg.Key, alg.IV);
        using var crypto = new CryptoStream(cipherStream, decryptor, CryptoStreamMode.Read);
        crypto.ScanAndWriteTo(Stream, BufferSize);
    }

}
