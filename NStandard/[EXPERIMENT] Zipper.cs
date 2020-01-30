#if EXPERIMENT
using System;
using System.Collections.Generic;

namespace NStandard
{
#if NETSTANDARD2_0
    public static class Zipper
    {
#region No selector
        public static IEnumerable<(T1, T2)> Create<T1, T2>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext())
                {
                    yield return (
                        e1.Current,
                        e2.Current);
                }
            }
        }

        public static IEnumerable<(T1, T2, T3)> Create<T1, T2, T3>(
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
                    yield return (
                        e1.Current,
                        e2.Current,
                        e3.Current);
                }
            }
        }

        public static IEnumerable<(T1, T2, T3, T4)> Create<T1, T2, T3, T4>(
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
                    yield return (
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current);
                }
            }
        }

        public static IEnumerable<(T1, T2, T3, T4, T5)> Create<T1, T2, T3, T4, T5>(
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
                    yield return (
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current);
                }
            }
        }

        public static IEnumerable<(T1, T2, T3, T4, T5, T6)> Create<T1, T2, T3, T4, T5, T6>(
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
                    yield return (
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current);
                }
            }
        }

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7)> Create<T1, T2, T3, T4, T5, T6, T7>(
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
                    yield return (
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

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8)> Create<T1, T2, T3, T4, T5, T6, T7, T8>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext())
                {
                    yield return (
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current);
                }
            }
        }

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9)> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            IEnumerable<T9> arg9)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            using (IEnumerator<T9> e9 = arg9.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext()
                    && e9.MoveNext())
                {
                    yield return (
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current,
                        e9.Current);
                }
            }
        }

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10)> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            IEnumerable<T9> arg9,
            IEnumerable<T10> arg10)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            using (IEnumerator<T9> e9 = arg9.GetEnumerator())
            using (IEnumerator<T10> e10 = arg10.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext()
                    && e9.MoveNext()
                    && e10.MoveNext())
                {
                    yield return (
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current,
                        e9.Current,
                        e10.Current);
                }
            }
        }

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11)> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            IEnumerable<T9> arg9,
            IEnumerable<T10> arg10,
            IEnumerable<T11> arg11)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            using (IEnumerator<T9> e9 = arg9.GetEnumerator())
            using (IEnumerator<T10> e10 = arg10.GetEnumerator())
            using (IEnumerator<T11> e11 = arg11.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext()
                    && e9.MoveNext()
                    && e10.MoveNext()
                    && e11.MoveNext())
                {
                    yield return (
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current,
                        e9.Current,
                        e10.Current,
                        e11.Current);
                }
            }
        }

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12)> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            IEnumerable<T9> arg9,
            IEnumerable<T10> arg10,
            IEnumerable<T11> arg11,
            IEnumerable<T12> arg12)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            using (IEnumerator<T9> e9 = arg9.GetEnumerator())
            using (IEnumerator<T10> e10 = arg10.GetEnumerator())
            using (IEnumerator<T11> e11 = arg11.GetEnumerator())
            using (IEnumerator<T12> e12 = arg12.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext()
                    && e9.MoveNext()
                    && e10.MoveNext()
                    && e11.MoveNext()
                    && e12.MoveNext())
                {
                    yield return (
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current,
                        e9.Current,
                        e10.Current,
                        e11.Current,
                        e12.Current);
                }
            }
        }

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13)> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            IEnumerable<T9> arg9,
            IEnumerable<T10> arg10,
            IEnumerable<T11> arg11,
            IEnumerable<T12> arg12,
            IEnumerable<T13> arg13)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            using (IEnumerator<T9> e9 = arg9.GetEnumerator())
            using (IEnumerator<T10> e10 = arg10.GetEnumerator())
            using (IEnumerator<T11> e11 = arg11.GetEnumerator())
            using (IEnumerator<T12> e12 = arg12.GetEnumerator())
            using (IEnumerator<T13> e13 = arg13.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext()
                    && e9.MoveNext()
                    && e10.MoveNext()
                    && e11.MoveNext()
                    && e12.MoveNext()
                    && e13.MoveNext())
                {
                    yield return (
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current,
                        e9.Current,
                        e10.Current,
                        e11.Current,
                        e12.Current,
                        e13.Current);
                }
            }
        }

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14)> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            IEnumerable<T9> arg9,
            IEnumerable<T10> arg10,
            IEnumerable<T11> arg11,
            IEnumerable<T12> arg12,
            IEnumerable<T13> arg13,
            IEnumerable<T14> arg14)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            using (IEnumerator<T9> e9 = arg9.GetEnumerator())
            using (IEnumerator<T10> e10 = arg10.GetEnumerator())
            using (IEnumerator<T11> e11 = arg11.GetEnumerator())
            using (IEnumerator<T12> e12 = arg12.GetEnumerator())
            using (IEnumerator<T13> e13 = arg13.GetEnumerator())
            using (IEnumerator<T14> e14 = arg14.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext()
                    && e9.MoveNext()
                    && e10.MoveNext()
                    && e11.MoveNext()
                    && e12.MoveNext()
                    && e13.MoveNext()
                    && e14.MoveNext())
                {
                    yield return (
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current,
                        e9.Current,
                        e10.Current,
                        e11.Current,
                        e12.Current,
                        e13.Current,
                        e14.Current);
                }
            }
        }

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15)> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            IEnumerable<T9> arg9,
            IEnumerable<T10> arg10,
            IEnumerable<T11> arg11,
            IEnumerable<T12> arg12,
            IEnumerable<T13> arg13,
            IEnumerable<T14> arg14,
            IEnumerable<T15> arg15)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            using (IEnumerator<T9> e9 = arg9.GetEnumerator())
            using (IEnumerator<T10> e10 = arg10.GetEnumerator())
            using (IEnumerator<T11> e11 = arg11.GetEnumerator())
            using (IEnumerator<T12> e12 = arg12.GetEnumerator())
            using (IEnumerator<T13> e13 = arg13.GetEnumerator())
            using (IEnumerator<T14> e14 = arg14.GetEnumerator())
            using (IEnumerator<T15> e15 = arg15.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext()
                    && e9.MoveNext()
                    && e10.MoveNext()
                    && e11.MoveNext()
                    && e12.MoveNext()
                    && e13.MoveNext()
                    && e14.MoveNext()
                    && e15.MoveNext())
                {
                    yield return (
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current,
                        e9.Current,
                        e10.Current,
                        e11.Current,
                        e12.Current,
                        e13.Current,
                        e14.Current,
                        e15.Current);
                }
            }
        }

        public static IEnumerable<(T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16)> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            IEnumerable<T9> arg9,
            IEnumerable<T10> arg10,
            IEnumerable<T11> arg11,
            IEnumerable<T12> arg12,
            IEnumerable<T13> arg13,
            IEnumerable<T14> arg14,
            IEnumerable<T15> arg15,
            IEnumerable<T16> arg16)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            using (IEnumerator<T9> e9 = arg9.GetEnumerator())
            using (IEnumerator<T10> e10 = arg10.GetEnumerator())
            using (IEnumerator<T11> e11 = arg11.GetEnumerator())
            using (IEnumerator<T12> e12 = arg12.GetEnumerator())
            using (IEnumerator<T13> e13 = arg13.GetEnumerator())
            using (IEnumerator<T14> e14 = arg14.GetEnumerator())
            using (IEnumerator<T15> e15 = arg15.GetEnumerator())
            using (IEnumerator<T16> e16 = arg16.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext()
                    && e9.MoveNext()
                    && e10.MoveNext()
                    && e11.MoveNext()
                    && e12.MoveNext()
                    && e13.MoveNext()
                    && e14.MoveNext()
                    && e15.MoveNext()
                    && e16.MoveNext())
                {
                    yield return (
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current,
                        e9.Current,
                        e10.Current,
                        e11.Current,
                        e12.Current,
                        e13.Current,
                        e14.Current,
                        e15.Current,
                        e16.Current);
                }
            }
        }
#endregion

#region Has selector
        public static IEnumerable<TRet> Create<T1, T2, TRet>(
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

        public static IEnumerable<TRet> Create<T1, T2, T3, TRet>(
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

        public static IEnumerable<TRet> Create<T1, T2, T3, T4, TRet>(
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

        public static IEnumerable<TRet> Create<T1, T2, T3, T4, T5, TRet>(
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

        public static IEnumerable<TRet> Create<T1, T2, T3, T4, T5, T6, TRet>(
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

        public static IEnumerable<TRet> Create<T1, T2, T3, T4, T5, T6, T7, TRet>(
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

        public static IEnumerable<TRet> Create<T1, T2, T3, T4, T5, T6, T7, T8, TRet>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, TRet> resultSelector)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext())
                {
                    yield return resultSelector(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current);
                }
            }
        }

        public static IEnumerable<TRet> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            IEnumerable<T9> arg9,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TRet> resultSelector)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            using (IEnumerator<T9> e9 = arg9.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext()
                    && e9.MoveNext())
                {
                    yield return resultSelector(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current,
                        e9.Current);
                }
            }
        }

        public static IEnumerable<TRet> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TRet>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            IEnumerable<T9> arg9,
            IEnumerable<T10> arg10,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TRet> resultSelector)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            using (IEnumerator<T9> e9 = arg9.GetEnumerator())
            using (IEnumerator<T10> e10 = arg10.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext()
                    && e9.MoveNext()
                    && e10.MoveNext())
                {
                    yield return resultSelector(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current,
                        e9.Current,
                        e10.Current);
                }
            }
        }

        public static IEnumerable<TRet> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TRet>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            IEnumerable<T9> arg9,
            IEnumerable<T10> arg10,
            IEnumerable<T11> arg11,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TRet> resultSelector)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            using (IEnumerator<T9> e9 = arg9.GetEnumerator())
            using (IEnumerator<T10> e10 = arg10.GetEnumerator())
            using (IEnumerator<T11> e11 = arg11.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext()
                    && e9.MoveNext()
                    && e10.MoveNext()
                    && e11.MoveNext())
                {
                    yield return resultSelector(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current,
                        e9.Current,
                        e10.Current,
                        e11.Current);
                }
            }
        }

        public static IEnumerable<TRet> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TRet>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            IEnumerable<T9> arg9,
            IEnumerable<T10> arg10,
            IEnumerable<T11> arg11,
            IEnumerable<T12> arg12,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TRet> resultSelector)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            using (IEnumerator<T9> e9 = arg9.GetEnumerator())
            using (IEnumerator<T10> e10 = arg10.GetEnumerator())
            using (IEnumerator<T11> e11 = arg11.GetEnumerator())
            using (IEnumerator<T12> e12 = arg12.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext()
                    && e9.MoveNext()
                    && e10.MoveNext()
                    && e11.MoveNext()
                    && e12.MoveNext())
                {
                    yield return resultSelector(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current,
                        e9.Current,
                        e10.Current,
                        e11.Current,
                        e12.Current);
                }
            }
        }

        public static IEnumerable<TRet> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TRet>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            IEnumerable<T9> arg9,
            IEnumerable<T10> arg10,
            IEnumerable<T11> arg11,
            IEnumerable<T12> arg12,
            IEnumerable<T13> arg13,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TRet> resultSelector)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            using (IEnumerator<T9> e9 = arg9.GetEnumerator())
            using (IEnumerator<T10> e10 = arg10.GetEnumerator())
            using (IEnumerator<T11> e11 = arg11.GetEnumerator())
            using (IEnumerator<T12> e12 = arg12.GetEnumerator())
            using (IEnumerator<T13> e13 = arg13.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext()
                    && e9.MoveNext()
                    && e10.MoveNext()
                    && e11.MoveNext()
                    && e12.MoveNext()
                    && e13.MoveNext())
                {
                    yield return resultSelector(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current,
                        e9.Current,
                        e10.Current,
                        e11.Current,
                        e12.Current,
                        e13.Current);
                }
            }
        }

        public static IEnumerable<TRet> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TRet>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            IEnumerable<T9> arg9,
            IEnumerable<T10> arg10,
            IEnumerable<T11> arg11,
            IEnumerable<T12> arg12,
            IEnumerable<T13> arg13,
            IEnumerable<T14> arg14,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TRet> resultSelector)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            using (IEnumerator<T9> e9 = arg9.GetEnumerator())
            using (IEnumerator<T10> e10 = arg10.GetEnumerator())
            using (IEnumerator<T11> e11 = arg11.GetEnumerator())
            using (IEnumerator<T12> e12 = arg12.GetEnumerator())
            using (IEnumerator<T13> e13 = arg13.GetEnumerator())
            using (IEnumerator<T14> e14 = arg14.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext()
                    && e9.MoveNext()
                    && e10.MoveNext()
                    && e11.MoveNext()
                    && e12.MoveNext()
                    && e13.MoveNext()
                    && e14.MoveNext())
                {
                    yield return resultSelector(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current,
                        e9.Current,
                        e10.Current,
                        e11.Current,
                        e12.Current,
                        e13.Current,
                        e14.Current);
                }
            }
        }

        public static IEnumerable<TRet> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TRet>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            IEnumerable<T9> arg9,
            IEnumerable<T10> arg10,
            IEnumerable<T11> arg11,
            IEnumerable<T12> arg12,
            IEnumerable<T13> arg13,
            IEnumerable<T14> arg14,
            IEnumerable<T15> arg15,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TRet> resultSelector)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            using (IEnumerator<T9> e9 = arg9.GetEnumerator())
            using (IEnumerator<T10> e10 = arg10.GetEnumerator())
            using (IEnumerator<T11> e11 = arg11.GetEnumerator())
            using (IEnumerator<T12> e12 = arg12.GetEnumerator())
            using (IEnumerator<T13> e13 = arg13.GetEnumerator())
            using (IEnumerator<T14> e14 = arg14.GetEnumerator())
            using (IEnumerator<T15> e15 = arg15.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext()
                    && e9.MoveNext()
                    && e10.MoveNext()
                    && e11.MoveNext()
                    && e12.MoveNext()
                    && e13.MoveNext()
                    && e14.MoveNext()
                    && e15.MoveNext())
                {
                    yield return resultSelector(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current,
                        e9.Current,
                        e10.Current,
                        e11.Current,
                        e12.Current,
                        e13.Current,
                        e14.Current,
                        e15.Current);
                }
            }
        }

        public static IEnumerable<TRet> Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TRet>(
            IEnumerable<T1> arg1,
            IEnumerable<T2> arg2,
            IEnumerable<T3> arg3,
            IEnumerable<T4> arg4,
            IEnumerable<T5> arg5,
            IEnumerable<T6> arg6,
            IEnumerable<T7> arg7,
            IEnumerable<T8> arg8,
            IEnumerable<T9> arg9,
            IEnumerable<T10> arg10,
            IEnumerable<T11> arg11,
            IEnumerable<T12> arg12,
            IEnumerable<T13> arg13,
            IEnumerable<T14> arg14,
            IEnumerable<T15> arg15,
            IEnumerable<T16> arg16,
            Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TRet> resultSelector)
        {
            using (IEnumerator<T1> e1 = arg1.GetEnumerator())
            using (IEnumerator<T2> e2 = arg2.GetEnumerator())
            using (IEnumerator<T3> e3 = arg3.GetEnumerator())
            using (IEnumerator<T4> e4 = arg4.GetEnumerator())
            using (IEnumerator<T5> e5 = arg5.GetEnumerator())
            using (IEnumerator<T6> e6 = arg6.GetEnumerator())
            using (IEnumerator<T7> e7 = arg7.GetEnumerator())
            using (IEnumerator<T8> e8 = arg8.GetEnumerator())
            using (IEnumerator<T9> e9 = arg9.GetEnumerator())
            using (IEnumerator<T10> e10 = arg10.GetEnumerator())
            using (IEnumerator<T11> e11 = arg11.GetEnumerator())
            using (IEnumerator<T12> e12 = arg12.GetEnumerator())
            using (IEnumerator<T13> e13 = arg13.GetEnumerator())
            using (IEnumerator<T14> e14 = arg14.GetEnumerator())
            using (IEnumerator<T15> e15 = arg15.GetEnumerator())
            using (IEnumerator<T16> e16 = arg16.GetEnumerator())
            {
                while (e1.MoveNext()
                    && e2.MoveNext()
                    && e3.MoveNext()
                    && e4.MoveNext()
                    && e5.MoveNext()
                    && e6.MoveNext()
                    && e7.MoveNext()
                    && e8.MoveNext()
                    && e9.MoveNext()
                    && e10.MoveNext()
                    && e11.MoveNext()
                    && e12.MoveNext()
                    && e13.MoveNext()
                    && e14.MoveNext()
                    && e15.MoveNext()
                    && e16.MoveNext())
                {
                    yield return resultSelector(
                        e1.Current,
                        e2.Current,
                        e3.Current,
                        e4.Current,
                        e5.Current,
                        e6.Current,
                        e7.Current,
                        e8.Current,
                        e9.Current,
                        e10.Current,
                        e11.Current,
                        e12.Current,
                        e13.Current,
                        e14.Current,
                        e15.Current,
                        e16.Current);
                }
            }
        }
#endregion

    }
#endif
}
#endif
