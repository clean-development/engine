using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;
using System.Collections.Generic;

namespace Components
{
	public static class ServiceExtensions
	{
		public static IServiceCollection UseComponentSystem(this IServiceCollection services)
		{
			services.TryAdd(DefaultServices());
			return services;
		}

		public static IEnumerable<ServiceDescriptor> DefaultServices()
		{
			yield return ServiceDescriptor.Singleton<StateManager, ConcurrentDictionaryStateManager>();
			yield return ServiceDescriptor.Transient<ComponentSystem, StateManagedComponentSystem>();
		}
	}
}
