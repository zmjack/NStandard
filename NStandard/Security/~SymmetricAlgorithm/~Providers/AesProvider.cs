using System;
using System.Linq;
using System.Security.Cryptography;

namespace NStandard.Security
{
    public class AesProvider : SymmetricAlgorithmProvider<AesProvider, Aes>
    {
        public static readonly byte[] EmptyIV = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public AesProvider(PaddingMode padding = PaddingMode.PKCS7) : base(padding)
        {
        }

        public AesProvider UseCBC(byte[] key)
        {
            Mode = CipherMode.CBC;
            return WithKey(key);
        }

        public AesProvider UseECB(byte[] key)
        {
            Mode = CipherMode.ECB;
            return WithKey(key);
        }

        public override AesProvider WithKey(byte[] key)
        {
            if (!new[] { 16, 24, 32 }.Contains(key.Length))
                throw new ArgumentException("The length of Key must be 16 or 24 or 32.", nameof(key));

            Key = key;
            return this;
        }

        public override Aes Create()
        {
            var aes = Aes.Create();
            aes.Mode = Mode;
            aes.Padding = Padding;
            aes.Key = Key.Clone() as byte[];

            if (Mode == CipherMode.ECB) aes.IV = EmptyIV;

            return aes;
        }

    }
}
