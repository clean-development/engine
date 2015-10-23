using Component.Tests.Fakes;
using Components;
using FluentAssertions;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Linq;
using Xunit;

namespace Component.Tests
{
    public class EventDispatcherTests
    {
        static IServiceProvider CreateProvider() => new ServiceCollection().AddInMemoryEventStream().BuildServiceProvider();
        static EventStream CreateEventStream(IServiceProvider serviceProvider = null) => (serviceProvider ?? CreateProvider()).GetRequiredService<EventStream>();
        static EventDispatcher CreateDispatcher(IServiceProvider serviceProvider = null) => (serviceProvider ?? CreateProvider()).GetRequiredService<EventDispatcher>();

        [Fact]
        public static void GivenANullEntityThrowArgumentNullException()
        {
            var producer = CreateDispatcher();

            Action act = () => producer.Dispatch<TestEvent>(null);

            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public static void GivenAnEventIsDispatchedThenEventCanBeReceievedFromTheEventStream()
        {
            var serviceProvider = CreateProvider();
            var stream = CreateEventStream(serviceProvider);
            var producer = CreateDispatcher(serviceProvider);
            var subscriber = new TestSubscriber<TestEvent>();
            var testEvent = new TestEvent();
            TestEvent receivedEvent;

            using (stream.Subscribe(subscriber))
            {
                producer.Dispatch(testEvent);
            }

            receivedEvent = subscriber.ReceivedEvents.FirstOrDefault();
            receivedEvent.Should().BeSameAs(testEvent);
        }
    }
}