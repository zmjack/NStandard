namespace NStandard;

public static partial class Any
{
    // Refer: https://github.com/dotnet/corefx/blob/a10890f4ffe0fadf090c922578ba0e606ebdd16c/src/Common/src/System/Text/StringOrCharArray.cs#L140
    public static class Text
    {
        /// <summary>
        /// Computes a deterministic hash code for a string.
        /// <para>(This should not be used anywhere there are concerns around hash-based attacks that would require a better code.)</para>
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>        
        public static unsafe int ComputeHashCode(string source)
        {
            fixed (char* s = source)
            {
                return GetHashCode(s, source.Length);
            }
        }

        /// <summary>
        /// Computes a deterministic hash code for a char array.
        /// <para>(This should not be used anywhere there are concerns around hash-based attacks that would require a better code.)</para>
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>        
        public static unsafe int ComputeHashCode(char[] chars)
        {
            fixed (char* s = chars)
            {
                return GetHashCode(s, chars.Length);
            }
        }

        /// <summary>
        /// Computes a deterministic hash code for a char array.
        /// <para>(This should not be used anywhere there are concerns around hash-based attacks that would require a better code.)</para>
        /// </summary>
        /// <param name="chars"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>        
        public static unsafe int ComputeHashCode(char[] chars, int offset, int count)
        {
            if (count > chars.Length - offset) throw new OverflowException($"The length of chars overflow.");

            fixed (char* s = chars)
            {
                return GetHashCode(s + offset, count);
            }
        }

        private static unsafe int GetHashCode(char* s, int count)
        {
            // This hash code is a simplified version of some of the code in String, 
            // when not using randomized hash codes.  We don't use string's GetHashCode
            // because we need to be able to use the exact same algorithms on a char[].
            // As such, this should not be used anywhere there are concerns around
            // hash-based attacks that would require a better code.

            int hash1 = (5381 << 16) + 5381;
            int hash2 = hash1;

            for (int i = 0; i < count; ++i)
            {
                int c = *s++;
                hash1 = unchecked((hash1 << 5) + hash1) ^ c;

                if (++i >= count)
                    break;

                c = *s++;
                hash2 = unchecked((hash2 << 5) + hash2) ^ c;
            }

            return unchecked(hash1 + (hash2 * 1566083941));
        }
    }
}
