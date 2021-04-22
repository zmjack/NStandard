using System.Security.Cryptography;

namespace NStandard.Security
{
    public class AesProvider : SymmetricProvider<AesCipher>
    {
        public override byte[] EmptyIV => new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        /// <summary>
        /// Create an AesProvider.
        /// </summary>
        /// <param name="cipher"></param>
        /// <param name="padding"></param>
        public AesProvider(CipherMode cipher = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7) : base(cipher, padding)
        {
        }

        /// <summary>
        /// Create an AesProvider.
        /// </summary>
        /// <param name="key">The length of Key must be 16, 24 or 32.</param>
        /// <param name="cipher"></param>
        /// <param name="padding"></param>
        public AesProvider(byte[] key, CipherMode cipher = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7) : base(key, cipher, padding)
        {
        }

        protected override SymmetricAlgorithm CreateAlgorithm() => Aes.Create();

    }
}
