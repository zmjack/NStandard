namespace NStandard.Security;

public class AesCipher : SymmetricCipher<AesCipher>
{
    public override int IVLength => 16;
}
