using System.Security.Cryptography;

namespace NStandard.Security;

public class DesProvider : SymmetricProvider<DesCipher>
{
    public override byte[] EmptyIV => [0, 0, 0, 0, 0, 0, 0, 0];

    /// <summary>
    /// Create a DesProvider.
    /// </summary>
    /// <param name="cipher"></param>
    /// <param name="padding"></param>
    public DesProvider(CipherMode cipher = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7) : base(cipher, padding)
    {
    }

    /// <summary>
    /// Create a DesProvider.
    /// </summary>
    /// <param name="key">The length of Key must be 8.</param>
    /// <param name="cipher"></param>
    /// <param name="padding"></param>
    public DesProvider(byte[] key, CipherMode cipher = CipherMode.CBC, PaddingMode padding = PaddingMode.PKCS7) : base(key, cipher, padding)
    {
    }

    protected override SymmetricAlgorithm CreateAlgorithm() => DES.Create();

}
