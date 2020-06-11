using System.Linq;

namespace NStandard.Security
{
    public class AesIVHandler
    {
        public static AesIVHandler Default = new AesIVHandler();

        public virtual byte[] Combine(CipherIVPair pair)
        {
            return pair.Cipher.Concat(pair.IV).ToArray();
        }

        public virtual CipherIVPair Separate(byte[] bytes)
        {
            var pair = new CipherIVPair
            {
                Cipher = bytes.Slice(0, -16),
                IV = bytes.Slice(-16),
            };
            return pair;
        }

    }
}
