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

		private static IServiceProvider CreateProvider()
			=> new ServiceCollection()
				.UseComponentSystem()
				.BuildServiceProvider();

		private static ComponentSystem CreateTarget(IServiceProvider provider = null)
			=> (provider ?? CreateProvider())
				.GetRequiredService<ComponentSystem>();

		[Fact]
		public static void WhenAssigningComponentToNullEntityThenThrowsException()
		{
			Action act = () => CreateTarget().Assign((object) null, new object());
			act.ShouldThrow<ArgumentNullException>();
		}

		[Fact]
		public static void WhenAssigningNullComponentToEntityThenThrowsException()
		{
			Action act = () => CreateTarget().Assign(new object(), (object)null);
			act.ShouldThrow<ArgumentNullException>();
		}

		[Fact]
		public static void WhenSameComponentTypeAssignedToSameEntityThenThrowsException()
		{
			var entity = new object();
			var component = new object();
			var provider = CreateProvider();
			Action act = () => CreateTarget(provider).Assign(entity, component);
			act.ShouldNotThrow<Exception>();
			act.ShouldThrow<ComponentAlreadyAssignedException>();
		}

		[Fact]
		public static void WhenTryingToAssignComponentToNullEntityThenReturnsFalse()
		{
			CreateTarget().TryAssign((object)null, new object())
				.Should().BeFalse();
		}

		[Fact]
		public static void WhenTryingToAssignNullComponentToEntityThenReturnsFalse()
		{
			CreateTarget().TryAssign(new object(), (object)null)
				.Should().BeFalse();
		}

		[Fact]
		public static void WhenTryingToAssignComponentToEntityThenReturnsTrue()
		{
			CreateTarget().TryAssign(new object(), new object())
				.Should().BeTrue();
		}
	}
}
