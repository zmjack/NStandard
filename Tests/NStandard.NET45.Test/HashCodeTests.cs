using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NStandard.NET45.Test
{
    [TestClass]
    public class HashCodeTests
    {
        [TestMethod]
        public void Test1()
        {
            var bytes = new byte[] { 1, 2, 3, 4, 5 };
            var first = HashCode.Combine(bytes);
            var second = HashCode.Combine(bytes);
            Assert.AreEqual(first, second);
        }
    }
}
