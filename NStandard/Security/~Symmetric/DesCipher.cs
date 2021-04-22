namespace NStandard.Security
{
    public class DesCipher : SymmetricCipher<DesCipher>
    {
        public override int IVLength => 8;
    }
}
