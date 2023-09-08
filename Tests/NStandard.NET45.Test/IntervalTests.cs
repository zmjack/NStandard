using Microsoft.VisualStudio.TestTools.UnitTesting;
using NStandard.Collections;
using System.Linq;

namespace NStandard.Test.Collections
{
    [TestClass]
    public class IntervalTests
    {
        private static readonly Interval<int> _model1 = new Interval<int>()
        {
            new Interval<int>.Range(4, 6),
            new Interval<int>.Range(10, 12),
            new Interval<int>.Range(16, 18),
        };
        private static readonly Interval<int> _model2 = new Interval<int>()
        {
            new Interval<int>.Range(5, 5),
            new Interval<int>.Range(11, 11),
            new Interval<int>.Range(17, 17),
        };

        private void AssertTrue(bool value) => Assert.AreEqual(true, value);
        private void AssertFalse(bool value) => Assert.AreEqual(false, value);
        private void AssertEmpty(Interval<int> left)
        {
            Assert.AreEqual(0, left.Count);
        }
        private void AssertEqual(Interval<int> left, Interval<int> right)
        {
            Assert.AreEqual(true, left.ToArray().SequenceEqual(right.ToArray()));
        }

        [TestMethod]
        public void AddTest1()
        {
            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 18),
            }, _model1 + new Interval<int>.Range(5, 17));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 14),
                new Interval<int>.Range(16, 18),
            }, _model1 + new Interval<int>.Range(5, 14));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 19),
            }, _model1 + new Interval<int>.Range(5, 19));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 6),
                new Interval<int>.Range(8, 18),
            }, _model1 + new Interval<int>.Range(8, 17));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 6),
                new Interval<int>.Range(8, 14),
                new Interval<int>.Range(16, 18),
            }, _model1 + new Interval<int>.Range(8, 14));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 6),
                new Interval<int>.Range(8, 19),
            }, _model1 + new Interval<int>.Range(8, 19));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(2, 18),
            }, _model1 + new Interval<int>.Range(2, 17));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(2, 14),
                new Interval<int>.Range(16, 18),
            }, _model1 + new Interval<int>.Range(2, 14));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(1, 6),
                new Interval<int>.Range(10, 12),
                new Interval<int>.Range(16, 18),
            }, _model1 + new Interval<int>.Range(1, 3));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(0, 2),
                new Interval<int>.Range(4, 6),
                new Interval<int>.Range(10, 12),
                new Interval<int>.Range(16, 18),
            }, _model1 + new Interval<int>.Range(0, 2));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 6),
                new Interval<int>.Range(10, 12),
                new Interval<int>.Range(16, 21),
            }, _model1 + new Interval<int>.Range(19, 21));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 6),
                new Interval<int>.Range(10, 12),
                new Interval<int>.Range(16, 18),
                new Interval<int>.Range(20, 22),
            }, _model1 + new Interval<int>.Range(20, 22));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(2, 20),
            }, _model1 + new Interval<int>.Range(2, 20));
        }

        [TestMethod]
        public void SubtractTest1()
        {
            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 4),
                new Interval<int>.Range(18, 18),
            }, _model1 - new Interval<int>.Range(5, 17));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 4),
                new Interval<int>.Range(16, 18),
            }, _model1 - new Interval<int>.Range(5, 14));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 4),
            }, _model1 - new Interval<int>.Range(5, 19));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 6),
                new Interval<int>.Range(18, 18),
            }, _model1 - new Interval<int>.Range(8, 17));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 6),
                new Interval<int>.Range(16, 18),
            }, _model1 - new Interval<int>.Range(8, 14));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 6),
            }, _model1 - new Interval<int>.Range(8, 19));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(18, 18),
            }, _model1 - new Interval<int>.Range(2, 17));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(16, 18),
            }, _model1 - new Interval<int>.Range(2, 14));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 6),
                new Interval<int>.Range(10, 12),
                new Interval<int>.Range(16, 18),
            }, _model1 - new Interval<int>.Range(1, 3));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 6),
                new Interval<int>.Range(10, 12),
                new Interval<int>.Range(16, 18),
            }, _model1 - new Interval<int>.Range(0, 2));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 6),
                new Interval<int>.Range(10, 12),
                new Interval<int>.Range(16, 18),
            }, _model1 - new Interval<int>.Range(19, 21));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(4, 6),
                new Interval<int>.Range(10, 12),
                new Interval<int>.Range(16, 18),
            }, _model1 - new Interval<int>.Range(20, 22));

            Assert.AreEqual(0, (_model1 - new Interval<int>.Range(2, 20)).Count);
        }

        [TestMethod]
        public void AddTest2()
        {
            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(5, 17),
            }, _model2 + new Interval<int>.Range(5, 17));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(5, 14),
                new Interval<int>.Range(17, 17),
            }, _model2 + new Interval<int>.Range(5, 14));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(5, 19),
            }, _model2 + new Interval<int>.Range(5, 19));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(5, 5),
                new Interval<int>.Range(8, 17),
            }, _model2 + new Interval<int>.Range(8, 17));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(5, 5),
                new Interval<int>.Range(8, 14),
                new Interval<int>.Range(17, 17),
            }, _model2 + new Interval<int>.Range(8, 14));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(5, 5),
                new Interval<int>.Range(8, 19),
            }, _model2 + new Interval<int>.Range(8, 19));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(2, 17),
            }, _model2 + new Interval<int>.Range(2, 17));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(2, 14),
                new Interval<int>.Range(17, 17),
            }, _model2 + new Interval<int>.Range(2, 14));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(1, 3),
                new Interval<int>.Range(5, 5),
                new Interval<int>.Range(11, 11),
                new Interval<int>.Range(17, 17),
            }, _model2 + new Interval<int>.Range(1, 3));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(0, 2),
                new Interval<int>.Range(5, 5),
                new Interval<int>.Range(11, 11),
                new Interval<int>.Range(17, 17),
            }, _model2 + new Interval<int>.Range(0, 2));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(5, 5),
                new Interval<int>.Range(11, 11),
                new Interval<int>.Range(17, 17),
                new Interval<int>.Range(19, 21),
            }, _model2 + new Interval<int>.Range(19, 21));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(5, 5),
                new Interval<int>.Range(11, 11),
                new Interval<int>.Range(17, 17),
                new Interval<int>.Range(20, 22),
            }, _model2 + new Interval<int>.Range(20, 22));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(2, 20),
            }, _model2 + new Interval<int>.Range(2, 20));
        }

        [TestMethod]
        public void SubtractTest2()
        {
            Assert.AreEqual(0, (_model2 - new Interval<int>.Range(5, 17)).Count);

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(17, 17),
            }, _model2 - new Interval<int>.Range(5, 14));

            AssertEmpty(_model2 - new Interval<int>.Range(5, 19));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(5, 5),
            }, _model2 - new Interval<int>.Range(8, 17));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(5, 5),
                new Interval<int>.Range(17, 17),
            }, _model2 - new Interval<int>.Range(8, 14));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(5, 5),
            }, _model2 - new Interval<int>.Range(8, 19));

            AssertEmpty(_model2 - new Interval<int>.Range(2, 17));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(17, 17),
            }, _model2 - new Interval<int>.Range(2, 14));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(5, 5),
                new Interval<int>.Range(11, 11),
                new Interval<int>.Range(17, 17),
            }, _model2 - new Interval<int>.Range(1, 3));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(5, 5),
                new Interval<int>.Range(11, 11),
                new Interval<int>.Range(17, 17),
            }, _model2 - new Interval<int>.Range(0, 2));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(5, 5),
                new Interval<int>.Range(11, 11),
                new Interval<int>.Range(17, 17),
            }, _model2 - new Interval<int>.Range(19, 21));

            AssertEqual(new Interval<int>
            {
                new Interval<int>.Range(5, 5),
                new Interval<int>.Range(11, 11),
                new Interval<int>.Range(17, 17),
            }, _model2 - new Interval<int>.Range(20, 22));

            AssertEmpty(_model2 - new Interval<int>.Range(2, 20));
        }

        [TestMethod]
        public void ContainsTest()
        {
            var interval = new Interval<int> { 1, 2, 4, 5, 6 };

            AssertFalse(interval.Contains(new Interval<int> { new Interval<int>.Range(1, 6) }));
            AssertFalse(interval.Contains(new Interval<int> { new Interval<int>.Range(2, 3) }));
            AssertFalse(interval.Contains(new Interval<int> { new Interval<int>.Range(2, 4) }));
            AssertTrue(interval.Contains(new Interval<int> { new Interval<int>.Range(1, 2) }));
            AssertTrue(interval.Contains(new Interval<int> { new Interval<int>.Range(4, 6) }));
            AssertTrue(interval.Contains(new Interval<int> { new Interval<int>.Range(5, 6) }));
        }

    }
}
