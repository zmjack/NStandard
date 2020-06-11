using System.Linq;

namespace NStandard.Security
{
    public class DesIVHandler
    {
        public static DesIVHandler Default = new DesIVHandler();

        public virtual byte[] Combine(CipherIVPair pair)
        {
            return pair.Cipher.Concat(pair.IV).ToArray();
        }

        public virtual CipherIVPair Separate(byte[] bytes)
        {
            var pair = new CipherIVPair
            {
                Cipher = bytes.Slice(0, -8),
                IV = bytes.Slice(-8),
            };
            return pair;
        }

    }
}
