
using infrastructure.services.background;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.configuration
{
    public static class BackgroundServicesConfiguration
    {
        public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        {
            services.AddHostedService<NetworkServerHostedService>();
            services.AddHostedService<OutboxMongoBackgroundService>();
            services.AddHostedService<QueueListenerBackgroundService>();
            
            return services;
        }
    }
}
