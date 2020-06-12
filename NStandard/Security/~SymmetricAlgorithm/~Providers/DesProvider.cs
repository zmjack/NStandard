using System;
using System.Security.Cryptography;

namespace NStandard.Security
{
    public class DesProvider : SymmetricAlgorithmProvider<DesProvider, DES>
    {
        public static readonly byte[] EmptyIV = { 0, 0, 0, 0, 0, 0, 0, 0 };

        public DesProvider(PaddingMode padding = PaddingMode.PKCS7) : base(padding)
        {
        }

        public DesProvider UseCBC(byte[] key)
        {
            Mode = CipherMode.CBC;
            return WithKey(key);
        }

        public DesProvider UseECB(byte[] key)
        {
            Mode = CipherMode.ECB;
            return WithKey(key);
        }

        protected override DesProvider WithKey(byte[] key)
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
