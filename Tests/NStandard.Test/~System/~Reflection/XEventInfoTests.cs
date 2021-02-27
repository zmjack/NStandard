using System;
using System.Collections.Generic;
using Xunit;

namespace NStandard.Test
{
    public class XEventInfo
    {
        private class OneClass
        {
            public static void Func(List<DateTime> list) => list.Add(DateTime.Now);
            public event Action<List<DateTime>> OnEvent;
            public void CallEvent(List<DateTime> list) => OnEvent?.Invoke(list);
        }

        [Fact]
        public void Test1()
        {
            var one = new OneClass();
            var eventType = typeof(OneClass).GetEvent(nameof(OneClass.OnEvent));
            var functionType = typeof(OneClass).GetMethod(nameof(OneClass.Func));

            one.OnEvent += OneClass.Func;
            eventType.AddEvent(one, functionType);

            var list = new List<DateTime>();
            one.CallEvent(list);
            Assert.Equal(2, list.Count);
        }

    }
}
