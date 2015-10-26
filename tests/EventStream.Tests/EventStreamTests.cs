using EventStream.Tests.Fakes;
using FluentAssertions;
using Microsoft.Framework.DependencyInjection;
using System;
using TestAttributes;

namespace EventStream.Tests
{
	public class EventStreamTests
	{
		private static IServiceProvider CreateProvider() 
			=> new ServiceCollection().AddInMemoryEventStream().BuildServiceProvider();
		private static EventStream CreateEventStream(IServiceProvider serviceProvider = null) 
			=> (serviceProvider ?? CreateProvider()).GetRequiredService<EventStream>();

		[Component]
		public static void GivenANullObserverBasedSubscriberThrowArgumentNullException()
		{
			var stream = CreateEventStream();

			Action act = () => stream.Subscribe(default(IObserver<TestEvent>));

			act.ShouldThrow<ArgumentNullException>();
		}

		[Component]
		public static void GivenANullActionBasedSubscriberThrowArgumentNullException()
		{
			var stream = CreateEventStream();

			Action act = () => stream.Subscribe(default(Action<TestEvent>));

			act.ShouldThrow<ArgumentNullException>();
		}

		[Component]
		public static void GivenAnObserverBasedSubscriberReturnsValidDisposable()
		{
			var stream = CreateEventStream();

			using (var subscription = stream.Subscribe(new TestSubscriber<TestEvent>()))
				subscription.Should().NotBeNull();
		}

		[Component]
		public static void GivenAnActionBasedSubscriberReturnsValidDisposable()
		{
			var stream = CreateEventStream();

			using (var subscription = stream.Subscribe((TestEvent e) => { }))
				subscription.Should().NotBeNull();
		}
	}
}
