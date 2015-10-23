using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.DependencyInjection.Extensions;

namespace EventStream
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddInMemoryEventStream(this IServiceCollection services)
        {
            services.TryAddSingleton<InMemoryEventStream, InMemoryEventStream>();
            services.TryAddTransient(typeof(EventStream), isp => isp.GetRequiredService<InMemoryEventStream>());
            services.TryAddTransient(typeof(EventDispatcher), isp => isp.GetRequiredService<InMemoryEventStream>());

            return services;
        }
    }
}
