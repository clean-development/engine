using Components;
using FluentAssertions;
using Microsoft.Framework.DependencyInjection;
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
    }
}
