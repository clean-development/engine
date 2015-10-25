using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace EventStream
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInMemoryEventStream(this IServiceCollection services)
        {
            services.TryAddSingleton<InMemoryEventStream, InMemoryEventStream>();
            services.TryAddTransient(typeof(EventStream), provider => provider.GetRequiredService<InMemoryEventStream>());
            services.TryAddTransient(typeof(EventDispatcher), provider => provider.GetRequiredService<InMemoryEventStream>());

            return services;
        }
    }
}
