using System;

namespace Components
{
    public interface EventStream
    {
        IDisposable Subscribe<T>(IObserver<T> subscriber);
        IDisposable Subscribe<T>(Action<T> callback);
    }
}
