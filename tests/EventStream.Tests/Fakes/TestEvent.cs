using System;

namespace EventStream.Tests.Fakes
{
    public class TestEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
