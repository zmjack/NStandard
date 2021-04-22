using System.Security.Cryptography;

namespace NStandard.Security
{
    public class TripleDesProvider : SymmetricProvider<TripleDesCipher>
    {
        public override byte[] EmptyIV => new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };

        /// <summary>
        /// Create a <see cref="TripleDesProvider"/>.
        /// </summary>
        /// <param name="cipher"></param>
        /// <param name="padding"></param>
        public TripleDesProvider(CipherMode cipher = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7) : base(cipher, padding)
        {
        }

        /// <summary>
        /// Create a <see cref="TripleDesProvider"/>.
        /// </summary>
        /// <param name="key">The length of Key must be 8.</param>
        /// <param name="cipher"></param>
        /// <param name="padding"></param>
        public TripleDesProvider(byte[] key, CipherMode cipher = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7) : base(key, cipher, padding)
        {
        }

        protected override SymmetricAlgorithm CreateAlgorithm() => TripleDES.Create();

    }
}
