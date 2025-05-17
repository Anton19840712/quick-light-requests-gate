
using domain.events;
using infrastructure.services.events;
using Microsoft.Extensions.DependencyInjection;

namespace infrastructure.configuration
{
    public static class EventConfiguration
    {
        public static IServiceCollection AddEventServices(this IServiceCollection services)
        {
            services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddScoped<IIncidentEventHandler, IncidentEventHandler>();
            services.AddScoped<IDomainEventHandler<IncidentCreatedEvent>, IncidentEventHandler>();
            services.AddScoped<IDomainEventHandler<IncidentUpdatedEvent>, IncidentEventHandler>();
            services.AddScoped<IDomainEventHandler<IncidentProcessedEvent>, IncidentEventHandler>();

            return services;
        }
    }
}
