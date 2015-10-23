using EventStream.Tests.Fakes;
using FluentAssertions;
using Microsoft.Framework.DependencyInjection;
using System;
using Xunit;

namespace EventStream.Tests
{
    public class EventStreamTests
    {
        static IServiceProvider CreateProvider() 
                                    => new ServiceCollection().AddInMemoryEventStream().BuildServiceProvider();
        static EventStream CreateEventStream(IServiceProvider serviceProvider = null) 
                                    => (serviceProvider ?? CreateProvider()).GetRequiredService<EventStream>();

        [Fact]
        public static void GivenANullObserverBasedSubscriberThrowArgumentNullException()
        {
            var stream = CreateEventStream();

            Action act = () => stream.Subscribe(default(IObserver<TestEvent>));

            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public static void GivenANullActionBasedSubscriberThrowArgumentNullException()
        {
            var stream = CreateEventStream();

            Action act = () => stream.Subscribe(default(Action<TestEvent>));

            act.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public static void GivenAnObserverBasedSubscriberReturnsValidDisposable()
        {
            var stream = CreateEventStream();

            using (var subscription = stream.Subscribe(new TestSubscriber<TestEvent>()))
                subscription.Should().NotBeNull();
        }

        [Fact]
        public static void GivenAnActionBasedSubscriberReturnsValidDisposable()
        {
            var stream = CreateEventStream();

            using (var subscription = stream.Subscribe((TestEvent e) => { }))
                subscription.Should().NotBeNull();
        }
    }
}
