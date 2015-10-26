using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace EntityComponentSystem
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddComponentSystem(this IServiceCollection services)
		{
			services.TryAddSingleton<StateManager, ConcurrentDictionaryStateManager>();
			services.TryAddTransient<ComponentSystem, StateManagedComponentSystem>();
			return services;
		}
	}
}
