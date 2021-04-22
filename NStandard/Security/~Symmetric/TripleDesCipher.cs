namespace NStandard.Security
{
    public class TripleDesCipher : SymmetricCipher<TripleDesCipher>
    {
        public override int IVLength => 8;
    }
}
