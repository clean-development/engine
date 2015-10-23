using System;
using System.Collections.Generic;

namespace EventStream.Tests.Fakes
{
    public class TestSubscriber<T> : IObserver<T>
    {
        public Queue<T> ReceivedEvents { get; } = new Queue<T>();

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext(T value)
        {
            if (value != null)
                ReceivedEvents.Enqueue(value);
        }
    }
}
