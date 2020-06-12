using System.IO;
using System.Security.Cryptography;

namespace NStandard.Security
{
    public abstract class SymmetricAlgorithmProvider<TSelf, TSymmetricAlgorithm>
        where TSymmetricAlgorithm : SymmetricAlgorithm
        where TSelf : SymmetricAlgorithmProvider<TSelf, TSymmetricAlgorithm>
    {
        public int BufferSize = 1024;
        public CipherMode Mode;
        public PaddingMode Padding;
        public byte[] Key;

        public SymmetricAlgorithmProvider(PaddingMode padding)
        {
            Padding = padding;
        }

        public abstract TSelf WithKey(byte[] key);

        public abstract TSymmetricAlgorithm Create();

        public CipherIVPair Encrypt(byte[] data)
        {
            using var stream = new MemoryStream(data);
            using var cipherStream = new MemoryStream();
            Encrypt(stream, cipherStream, out var iv);

            var pair = new CipherIVPair
            {
                Cipher = cipherStream.ToArray(),
                IV = iv,
            };
            return pair;
        }
        public void Encrypt(Stream stream, Stream cipherStream, out byte[] iv)
        {
            var alg = Create();
            iv = alg.IV.Clone() as byte[];
            using (var encryptor = alg.CreateEncryptor())
            using (var crypto = new CryptoStream(cipherStream, encryptor, CryptoStreamMode.Write))
            {
                stream.ScanAndWriteTo(crypto, BufferSize);
            }
        }

        public byte[] Decrypt(CipherIVPair pair)
        {
            using var cipherStream = new MemoryStream(pair.Cipher);
            using var stream = new MemoryStream();
            Decrypt(cipherStream, stream, pair.IV);
            return stream.ToArray();
        }
        public void Decrypt(Stream cipherStream, Stream Stream, byte[] iv)
        {
            var alg = Create();
            alg.IV = iv.Clone() as byte[];
            using (var decryptor = alg.CreateDecryptor(alg.Key, alg.IV))
            using (var crypto = new CryptoStream(cipherStream, decryptor, CryptoStreamMode.Read))
            {
                crypto.ScanAndWriteTo(Stream, BufferSize);
            }
        }

    }
}
