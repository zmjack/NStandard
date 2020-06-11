using System;
using System.Linq;
using System.Security.Cryptography;

namespace NStandard.Security
{
    public class AesProvider : SymmetricAlgorithmProvider<AesProvider, Aes>
    {
        public static readonly byte[] EmptyIV = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public AesProvider UseCBC(PaddingMode padding = PaddingMode.PKCS7)
        {
            Mode = CipherMode.CBC;
            Padding = padding;
            return this;
        }

        public AesProvider UseECB(PaddingMode padding = PaddingMode.PKCS7)
        {
            Mode = CipherMode.ECB;
            Padding = padding;
            return this;
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
