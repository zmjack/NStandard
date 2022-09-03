using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace NStandard.NET45.Test
{
    [TestClass]
    public class TupleTests
    {
        [TestMethod]
        public void Test1()
        {
            var arr = new[]
            {
                Tuple.Create(3, 2),
                Tuple.Create(3, 1),
            };
            var ordered = arr.OrderBy(x => x).ToArray();
            Assert.AreEqual(1, ordered[0].Item2);
            Assert.AreEqual(2, ordered[1].Item2);
        }
    }
}
