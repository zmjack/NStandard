using System;
using System.Linq;
using System.Security.Cryptography;

namespace NStandard.Security
{
    public class TripleDesProvider : SymmetricAlgorithmProvider<TripleDesProvider, TripleDES>
    {
        public static readonly byte[] EmptyIV = { 0, 0, 0, 0, 0, 0, 0, 0 };

        public TripleDesProvider(PaddingMode padding = PaddingMode.PKCS7) : base(padding)
        {
        }

        public TripleDesProvider UseCBC(byte[] key)
        {
            Mode = CipherMode.CBC;
            return WithKey(key);
        }

        public TripleDesProvider UseECB(byte[] key)
        {
            Mode = CipherMode.ECB;
            return WithKey(key);
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
