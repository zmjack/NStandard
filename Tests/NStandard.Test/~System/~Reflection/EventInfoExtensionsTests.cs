using System;
using System.Collections.Generic;
using Xunit;

namespace NStandard.Test
{
    public class EventInfoExtensionsTests
    {
        private class OneClass
        {
            public static void Func(List<DateTime> list) => list.Add(DateTime.Now);
            public event Action<List<DateTime>> Event;
            public void CallEvent(List<DateTime> list) => Event?.Invoke(list);
        }

        [Fact]
        public void Test1()
        {
            var one = new OneClass();
            var eventType = typeof(OneClass).GetEvent(nameof(OneClass.Event));
            var functionType = typeof(OneClass).GetMethod(nameof(OneClass.Func));

            one.Event += OneClass.Func;
            eventType.AddEventHandler(one, functionType);

            var list = new List<DateTime>();
            one.CallEvent(list);
            Assert.Equal(2, list.Count);
        }

    }
}
