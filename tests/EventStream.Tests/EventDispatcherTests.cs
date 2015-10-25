using EventStream.Tests.Fakes;
using FluentAssertions;
using Microsoft.Framework.DependencyInjection;
using System;
using System.Linq;
using TestAttributes;

namespace EventStream.Tests
{
	public class EventDispatcherTests
	{
		private static IServiceProvider CreateProvider() 
			=> new ServiceCollection().AddInMemoryEventStream().BuildServiceProvider();
		private static EventStream CreateEventStream(IServiceProvider serviceProvider = null) 
			=> (serviceProvider ?? CreateProvider()).GetRequiredService<EventStream>();
		private static EventDispatcher CreateDispatcher(IServiceProvider serviceProvider = null) 
			=> (serviceProvider ?? CreateProvider()).GetRequiredService<EventDispatcher>();

		[Unit]
		public static void GivenANullEntityThrowArgumentNullException()
		{
			var producer = CreateDispatcher();

			Action act = () => producer.Dispatch<TestEvent>(null);

			act.ShouldThrow<ArgumentNullException>();
		}

		[Unit]
		public static void GivenAnEventIsDispatchedThenEventCanBeReceievedFromTheEventStream()
		{
			var serviceProvider = CreateProvider();
			var stream = CreateEventStream(serviceProvider);
			var producer = CreateDispatcher(serviceProvider);
			var subscriber = new TestSubscriber<TestEvent>();
			var testEvent = new TestEvent();
			TestEvent receivedEvent;

			using (stream.Subscribe(subscriber))
				producer.Dispatch(testEvent);

			receivedEvent = subscriber.ReceivedEvents.FirstOrDefault();
			receivedEvent.Should().BeSameAs(testEvent);
		}
	}
}