using System;
using System.Linq;
using System.Security.Cryptography;

namespace NStandard.Security
{
    public class TripleDesProvider : SymmetricAlgorithmProvider<TripleDesProvider, TripleDES>
    {
        public static readonly byte[] EmptyIV = { 0, 0, 0, 0, 0, 0, 0, 0 };

        public TripleDesProvider UseCBC(PaddingMode padding = PaddingMode.PKCS7)
        {
            Mode = CipherMode.CBC;
            Padding = padding;
            return this;
        }

        public TripleDesProvider UseECB(PaddingMode padding = PaddingMode.PKCS7)
        {
            Mode = CipherMode.ECB;
            Padding = padding;
            return this;
        }

        public override TripleDesProvider WithKey(byte[] key)
        {
            if (!new[] { 16, 24 }.Contains(key.Length))
                throw new ArgumentException("The length of Key must be 16 or 24.", nameof(key));

            Key = key;
            return this;
        }

        public override TripleDES Create()
        {
            var tripleDES = TripleDES.Create();
            tripleDES.Mode = Mode;
            tripleDES.Padding = Padding;
            tripleDES.Key = Key.Clone() as byte[];

            if (Mode == CipherMode.ECB) tripleDES.IV = EmptyIV;

            return tripleDES;
        }

    }
}
