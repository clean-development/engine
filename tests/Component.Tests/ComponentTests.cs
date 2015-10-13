using Components;
using FluentAssertions;
using Microsoft.Framework.DependencyInjection;
using System;
using Xunit;

namespace Component.Tests
{
	public class ComponentTests
	{
		[Fact]
		public static void WhenServiceAddedThenCanResolveComponentSystem()
		{
			new ServiceCollection()
				.UseComponentSystem()
				.BuildServiceProvider()
				.GetService<ComponentSystem>()
				.Should().NotBeNull();
		}

		private static ComponentSystem CreateTarget()
		{
			return new ServiceCollection()
				.UseComponentSystem()
				.BuildServiceProvider()
				.GetRequiredService<ComponentSystem>();
		}

		[Fact]
		public static void WhenAssigningComponentToNullEntityThenThrowsException()
		{
			Action act = () => CreateTarget().Assign<object, object>(null, new object());
			act.ShouldThrow<ArgumentNullException>();
		}

		[Fact]
		public static void WhenAssigningNullComponentToEntityThenThrowsException()
		{
			Action act = () => CreateTarget().Assign<object, object>(new object(), null);
			act.ShouldThrow<ArgumentNullException>();
		}

		[Fact]
		public static void WhenSameComponentTypeAssignedToSameEntityThenThrowsException()
		{
			var entity = new object();
			var component = new object();
			Action act = () => CreateTarget().Assign(entity, component);
			act.ShouldNotThrow<Exception>();
			act.ShouldThrow<ComponentAlreadyAssignedException>();
		}
	}
}
