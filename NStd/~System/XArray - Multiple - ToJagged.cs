namespace NStd
{
    public static partial class XArray
    {
        #region Multidimensional Array ToJaggedArray
        /// <summary>
        /// Converts jagged array to multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T[][] ToJaggedArray<T>(this T[,] @this)
        {
            var ret = new T[@this.GetLength(0)][];
            for (int i0 = 0; i0 < @this.Length; i0++)
            {
                ret[i0] = new T[@this.GetLength(1)];
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                {
                    ret[i0][i1] = @this[i0, i1];
                }
            }
            return ret;
        }

        /// <summary>
        /// Converts jagged array to multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T[][][] ToJaggedArray<T>(this T[,,] @this)
        {
            var ret = new T[@this.GetLength(0)][][];
            for (int i0 = 0; i0 < @this.Length; i0++)
            {
                ret[i0] = new T[@this.GetLength(1)][];
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                {
                    ret[i0][i1] = new T[@this.GetLength(2)];
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                    {
                        ret[i0][i1][i2] = @this[i0, i1, i2];
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Converts jagged array to multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T[][][][] ToJaggedArray<T>(this T[,,,] @this)
        {
            var ret = new T[@this.GetLength(0)][][][];
            for (int i0 = 0; i0 < @this.Length; i0++)
            {
                ret[i0] = new T[@this.GetLength(1)][][];
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                {
                    ret[i0][i1] = new T[@this.GetLength(2)][];
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                    {
                        ret[i0][i1][i2] = new T[@this.GetLength(3)];
                        for (int i3 = 0; i3 < @this.GetLength(3); i3++)
                        {
                            ret[i0][i1][i2][i3] = @this[i0, i1, i2, i3];
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Converts jagged array to multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T[][][][][] ToJaggedArray<T>(this T[,,,,] @this)
        {
            var ret = new T[@this.GetLength(0)][][][][];
            for (int i0 = 0; i0 < @this.Length; i0++)
            {
                ret[i0] = new T[@this.GetLength(1)][][][];
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                {
                    ret[i0][i1] = new T[@this.GetLength(2)][][];
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                    {
                        ret[i0][i1][i2] = new T[@this.GetLength(3)][];
                        for (int i3 = 0; i3 < @this.GetLength(3); i3++)
                        {
                            ret[i0][i1][i2][i3] = new T[@this.GetLength(4)];
                            for (int i4 = 0; i4 < @this.GetLength(4); i4++)
                            {
                                ret[i0][i1][i2][i3][i4] = @this[i0, i1, i2, i3, i4];
                            }
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Converts jagged array to multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T[][][][][][] ToJaggedArray<T>(this T[,,,,,] @this)
        {
            var ret = new T[@this.GetLength(0)][][][][][];
            for (int i0 = 0; i0 < @this.Length; i0++)
            {
                ret[i0] = new T[@this.GetLength(1)][][][][];
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                {
                    ret[i0][i1] = new T[@this.GetLength(2)][][][];
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                    {
                        ret[i0][i1][i2] = new T[@this.GetLength(3)][][];
                        for (int i3 = 0; i3 < @this.GetLength(3); i3++)
                        {
                            ret[i0][i1][i2][i3] = new T[@this.GetLength(4)][];
                            for (int i4 = 0; i4 < @this.GetLength(4); i4++)
                            {
                                ret[i0][i1][i2][i3][i4] = new T[@this.GetLength(5)];
                                for (int i5 = 0; i5 < @this.GetLength(5); i5++)
                                {
                                    ret[i0][i1][i2][i3][i4][i5] = @this[i0, i1, i2, i3, i4, i5];
                                }
                            }
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Converts jagged array to multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T[][][][][][][] ToJaggedArray<T>(this T[,,,,,,] @this)
        {
            var ret = new T[@this.GetLength(0)][][][][][][];
            for (int i0 = 0; i0 < @this.Length; i0++)
            {
                ret[i0] = new T[@this.GetLength(1)][][][][][];
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                {
                    ret[i0][i1] = new T[@this.GetLength(2)][][][][];
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                    {
                        ret[i0][i1][i2] = new T[@this.GetLength(3)][][][];
                        for (int i3 = 0; i3 < @this.GetLength(3); i3++)
                        {
                            ret[i0][i1][i2][i3] = new T[@this.GetLength(4)][][];
                            for (int i4 = 0; i4 < @this.GetLength(4); i4++)
                            {
                                ret[i0][i1][i2][i3][i4] = new T[@this.GetLength(5)][];
                                for (int i5 = 0; i5 < @this.GetLength(5); i5++)
                                {
                                    ret[i0][i1][i2][i3][i4][i5] = new T[@this.GetLength(6)];
                                    for (int i6 = 0; i6 < @this.GetLength(6); i6++)
                                    {
                                        ret[i0][i1][i2][i3][i4][i5][i6] = @this[i0, i1, i2, i3, i4, i5, i6];
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ret;
        }

        /// <summary>
        /// Converts jagged array to multidimensional array.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static T[][][][][][][][] ToJaggedArray<T>(this T[,,,,,,,] @this)
        {
            var ret = new T[@this.GetLength(0)][][][][][][][];
            for (int i0 = 0; i0 < @this.Length; i0++)
            {
                ret[i0] = new T[@this.GetLength(1)][][][][][][];
                for (int i1 = 0; i1 < @this.GetLength(1); i1++)
                {
                    ret[i0][i1] = new T[@this.GetLength(2)][][][][][];
                    for (int i2 = 0; i2 < @this.GetLength(2); i2++)
                    {
                        ret[i0][i1][i2] = new T[@this.GetLength(3)][][][][];
                        for (int i3 = 0; i3 < @this.GetLength(3); i3++)
                        {
                            ret[i0][i1][i2][i3] = new T[@this.GetLength(4)][][][];
                            for (int i4 = 0; i4 < @this.GetLength(4); i4++)
                            {
                                ret[i0][i1][i2][i3][i4] = new T[@this.GetLength(5)][][];
                                for (int i5 = 0; i5 < @this.GetLength(5); i5++)
                                {
                                    ret[i0][i1][i2][i3][i4][i5] = new T[@this.GetLength(6)][];
                                    for (int i6 = 0; i6 < @this.GetLength(6); i6++)
                                    {
                                        ret[i0][i1][i2][i3][i4][i5][i6] = new T[@this.GetLength(7)];
                                        for (int i7 = 0; i7 < @this.GetLength(7); i7++)
                                        {
                                            ret[i0][i1][i2][i3][i4][i5][i6][i7] = @this[i0, i1, i2, i3, i4, i5, i6, i7];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return ret;
        }
        #endregion
    }
}
