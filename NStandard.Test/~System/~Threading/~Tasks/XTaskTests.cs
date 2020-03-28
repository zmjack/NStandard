﻿using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NStandard.Test
{
    public class XTaskTests
    {
        [Fact]
        public void CatchTest()
        {
            var task = Task.Run(() =>
            {
                Thread.Sleep(1000);
                throw new InvalidOperationException("Exception...");
            });

            Exception exception = null;
            task.Catch(ex => exception = ex);
            Assert.Equal("Exception...", exception.InnerException.Message);
        }

    }
}