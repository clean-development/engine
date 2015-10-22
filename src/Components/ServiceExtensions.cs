using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace Components
{
	public static class ServiceExtensions
	{
		public static IServiceCollection AddComponentSystem(this IServiceCollection services)
		{
			services.TryAddSingleton<StateManager, ConcurrentDictionaryStateManager>();
			services.TryAddTransient<ComponentSystem, StateManagedComponentSystem>();
			return services;
		}

        public static IServiceCollection AddInMemoryEventStream(this IServiceCollection services)
        {
            var inMemoryInstance = new InMemoryEventStream();

            services.TryAdd(ServiceDescriptor.Instance(typeof(EventStream), inMemoryInstance));
            services.TryAdd(ServiceDescriptor.Instance(typeof(EventDispatcher), inMemoryInstance));

            return services;
        }
	}
}
