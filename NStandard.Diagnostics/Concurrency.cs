using System;
#if NET35
using System.Collections.Generic;
#else
using System.Collections.Concurrent;
#endif
using System.Diagnostics;
using System.Threading;

namespace NStandard.Diagnostics
{
    public static class Concurrency
    {
        /// <summary>
        /// Use mutil-thread to simulate concurrent scenarios.
        /// </summary>
        /// <typeparam name="TRet"></typeparam>
        /// <param name="task"></param>
        /// <param name="level"></param>
        /// <param name="threadCount">If the value is 0, <see cref="Environment.ProcessorCount"/> will be used.</param>
        /// <returns></returns>
        public static TestReport<TRet> Run<TRet>(Func<TestId, TRet> task, int level, int threadCount = 0)
        {
            if (level < 1) throw new ArgumentException($"The `{nameof(level)}` must be greater than 0.", nameof(level));
            if (threadCount < 0) throw new ArgumentException($"The `{nameof(threadCount)}` must be non-negative.", nameof(threadCount));
            if (threadCount == 0) threadCount = Environment.ProcessorCount;

            var div = level / threadCount;
            var mod = level % threadCount;
            threadCount = Math.Min(level, threadCount);

#if NET35
            var results = new List<TestResult<TRet>>();
#else
            var results = new ConcurrentBag<TestResult<TRet>>();
#endif
            var threads = new Thread[threadCount];
            foreach (var threadNumber in new int[threadCount].Let(i => i))
            {
                threads[threadNumber] = new Thread(() =>
                {
                    var s_count = threadNumber < mod ? div + 1 : div;
                    for (int invokeNumber = 0; invokeNumber < s_count; invokeNumber++)
                    {
                        var threadId = Thread.CurrentThread.ManagedThreadId;
                        var stopwatch = new Stopwatch();

                        stopwatch.Start();
                        try
                        {
                            var taskRet = task(new TestId(threadId, invokeNumber));
                            stopwatch.Stop();
                            var id = new TestId(threadId, invokeNumber);
                            var result = new TestResult<TRet>
                            {
                                ThreadId = threadId,
                                InvokeNumber = invokeNumber,
                                Success = true,

                                Return = taskRet,
                                Elapsed = stopwatch.Elapsed,
                                Exception = null,
                            };
#if NET35
                            lock (results)
                            {
                                results.Add(result);
                            }
#else
                            results.Add(result);
#endif
                        }
                        catch (Exception ex)
                        {
                            stopwatch.Stop();
                            var id = new TestId(threadId, invokeNumber);
                            var result = new TestResult<TRet>
                            {
                                ThreadId = threadId,
                                InvokeNumber = invokeNumber,
                                Success = false,

                                Return = default,
                                Elapsed = stopwatch.Elapsed,
                                Exception = ex,
                            };
#if NET35
                            lock (results)
                            {
                                results.Add(result);
                            }
#else
                            results.Add(result);
#endif
                        }
                    }
                });
            }

            foreach (var thread in threads) thread.Start();

            do { Thread.Sleep(500); }
            while (results.Count != level);

            return new TestReport<TRet>(results.ToArray());
        }

    }
}
