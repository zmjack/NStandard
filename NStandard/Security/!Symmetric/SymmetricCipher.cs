namespace NStandard.Security;

public abstract class SymmetricCipher<TSelf> where TSelf : SymmetricCipher<TSelf>, new()
{
    public abstract int IVLength { get; }
    public byte[]? Cipher { get; set; }
    public byte[]? IV { get; set; }

    public byte[] ToBytes()
    {
        return [.. Cipher, .. IV];
    }

    public TSelf FromBytes(byte[] bytes)
    {
        var pair = new TSelf
        {
            Cipher = bytes.Slice(0, -IVLength),
            IV = bytes.Slice(-IVLength),
        };
        return pair;
    }
}
