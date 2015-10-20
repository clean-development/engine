using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace Components
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddComponentSystem(this IServiceCollection services)
		{
			services.TryAddTransient<StateManager, ConcurrentDictionaryStateManager>();
			services.TryAddTransient<ComponentSystem, StateManagedComponentSystem>();
			return services;
		}
	}
}
