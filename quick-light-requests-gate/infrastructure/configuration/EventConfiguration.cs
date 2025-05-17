
using domain.events;
using infrastructure.services.events;
using quick_light_requests_gate.infrastructure.services.events;
using quick_light_requests_gate.tmp;

namespace infrastructure.configuration
{
    public static class EventConfiguration
    {
        public static IServiceCollection AddEventServices(this IServiceCollection services)
        {
            services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddScoped<IDomainEventHandler<IncidentCreatedEvent>, IncidentEventHandler>();
            services.AddScoped<IDomainEventHandler<IncidentUpdatedEvent>, IncidentEventHandler>();
            services.AddScoped<IDomainEventHandler<IncidentProcessedEvent>, IncidentEventHandler>();
            services.AddScoped<IEventPublisher, EventPublisher>();
            services.AddScoped<IDomainEventHandler<IncidentCreatedEvent>, IncidentEventHandler>();

			return services;
        }
    }
}
