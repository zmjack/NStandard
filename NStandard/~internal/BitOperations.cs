namespace NStandard
{
    internal class BitOperations
    {
        public static uint RotateLeft(uint value, int offset)
        {
            return (value << offset) | (value >> (32 - offset));
        }

        public static uint RotateRight(uint value, int offset)
        {
            return (value >> offset) | (value << (32 - offset));
        }

    }
}
