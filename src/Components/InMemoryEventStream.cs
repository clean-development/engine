using System;
using System.Collections.Generic;
using System.Linq;

namespace Components
{
    internal sealed class InMemoryEventStream : EventStream, EventDispatcher
    {
        private readonly HashSet<object> _subscriptions = new HashSet<object>();

        public void Dispatch<T>(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            lock(_subscriptions)
                foreach (var subscription in _subscriptions.OfType<IObserver<T>>())
                    subscription.OnNext(entity);
        }

        public IDisposable Subscribe<T>(Action<T> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            return Subscribe(new CallbackBasedObserver<T>(callback));
        }

        public IDisposable Subscribe<T>(IObserver<T> subscriber)
        {
            if (subscriber == null)
                throw new ArgumentNullException(nameof(subscriber));

            lock(_subscriptions)
                _subscriptions.Add(subscriber);

            return new Subscription(() => Unsubscribe(subscriber));
        }

        private void Unsubscribe<T>(IObserver<T> subscriber)
        {
            lock (_subscriptions)
                _subscriptions.Remove(subscriber);
        }

        private class CallbackBasedObserver<T> : IObserver<T>
        {
            private readonly Action<T> _callback;

            internal CallbackBasedObserver(Action<T> callback)
            {
                _callback = callback;
            }
            public void OnCompleted()
            {
            }
            public void OnError(Exception error)
            {
            }
            public void OnNext(T value) => _callback(value);
        }
        private class Subscription : IDisposable
        {
            private Action _unsubscribe;

            internal Subscription(Action unsubscribe)
            {
                _unsubscribe = unsubscribe;
            }

            public void Dispose() => _unsubscribe?.Invoke();
        }
    }
}
