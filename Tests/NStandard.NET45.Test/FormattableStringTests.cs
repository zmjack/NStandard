using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Runtime.CompilerServices;

namespace NStandard.NET45.Test
{
    [TestClass]
    public class FormattableStringTests
    {
        [TestMethod]
        public void FormattableStringTest()
        {
            var now = DateTime.Now;
            var formattable = FormattableStringFactory.Create("Hello NStandard. ({0})", now);
            Assert.AreEqual($"Hello NStandard. ({now})", formattable.ToString());
        }
    }
}
