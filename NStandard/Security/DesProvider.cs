using System;
using System.Security.Cryptography;

namespace NStandard.Security
{
    public class DesProvider : SymmetricAlgorithmProvider<DesProvider, DES>
    {
        public static readonly byte[] EmptyIV = { 0, 0, 0, 0, 0, 0, 0, 0 };

        public DesProvider UseCBC(PaddingMode padding = PaddingMode.PKCS7)
        {
            Mode = CipherMode.CBC;
            Padding = padding;
            return this;
        }

        public DesProvider UseECB(PaddingMode padding = PaddingMode.PKCS7)
        {
            Mode = CipherMode.ECB;
            Padding = padding;
            return this;
        }

        public override DesProvider WithKey(byte[] key)
        {
            if (key.Length != 8)
                throw new ArgumentException("The length of Key must be 8.", nameof(key));

            Key = key;
            return this;
        }

        public override DES Create()
        {
            var des = DES.Create();
            des.Mode = Mode;
            des.Padding = Padding;
            des.Key = Key.Clone() as byte[];

            if (Mode == CipherMode.ECB) des.IV = EmptyIV;

            return des;
        }

    }
}
