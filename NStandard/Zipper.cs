using System;
using System.Collections.Generic;

namespace NStandard
{
    public static class Zipper
    {
        #region No selector
        public static IEnumerable<Tuple<T1, T2>> Create<T1, T2>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext())
                {
                    yield return Tuple.Create(
                        e1.Current,
                        e2.Current);
                }
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3>> Create<T1, T2, T3>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext())
                {
                    yield return Tuple.Create(
                        e1.Current,
                        e2.Current,
                        e3.Current);
                }
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3, T4>> Create<T1, T2, T3, T4>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext())
                {
                    yield return Tuple.Create(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current);
                }
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3, T4, T5>> Create<T1, T2, T3, T4, T5>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext())
                {
                    yield return Tuple.Create(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current);
                }
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3, T4, T5, T6>> Create<T1, T2, T3, T4, T5, T6>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext())
                {
                    yield return Tuple.Create(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current);
                }
            }
        }

        public static IEnumerable<Tuple<T1, T2, T3, T4, T5, T6, T7>> Create<T1, T2, T3, T4, T5, T6, T7>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext())
                {
                    yield return Tuple.Create(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current);
                }
            }
        }
        #endregion

        #region Has selector
        public static IEnumerable<TRet> Select<T1, T2, TRet>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            Func<T1, T2, TRet> resultSelector)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext())
                {
                    yield return resultSelector(
                        e1.Current,
                        e2.Current);
                }
            }
        }

        public static IEnumerable<TRet> Select<T1, T2, T3, TRet>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            Func<T1, T2, T3, TRet> resultSelector)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext())
                {
                    yield return resultSelector(
                        e1.Current,
                        e2.Current,
                        e3.Current);
                }
            }
        }

        public static IEnumerable<TRet> Select<T1, T2, T3, T4, TRet>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            Func<T1, T2, T3, T4, TRet> resultSelector)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext())
                {
                    yield return resultSelector(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current);
                }
            }
        }

#if !NET35
        public static IEnumerable<TRet> Select<T1, T2, T3, T4, T5, TRet>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            Func<T1, T2, T3, T4, T5, TRet> resultSelector)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext())
                {
                    yield return resultSelector(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current);
                }
            }
        }

        public static IEnumerable<TRet> Select<T1, T2, T3, T4, T5, T6, TRet>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            Func<T1, T2, T3, T4, T5, T6, TRet> resultSelector)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext())
                {
                    yield return resultSelector(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current);
                }
            }
        }

        public static IEnumerable<TRet> Select<T1, T2, T3, T4, T5, T6, T7, TRet>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            Func<T1, T2, T3, T4, T5, T6, T7, TRet> resultSelector)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext())
                {
                    yield return resultSelector(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current);
                }
            }
        }
#endif
        #endregion

    }
}