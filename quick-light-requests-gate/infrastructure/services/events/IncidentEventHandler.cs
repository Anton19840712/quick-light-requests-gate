using domain.events;
using quick_light_requests_gate.tmp;

namespace quick_light_requests_gate.infrastructure.services.events
{
	public class IncidentEventHandler :
		IDomainEventHandler<IncidentCreatedEvent>,
		IDomainEventHandler<IncidentUpdatedEvent>,
		IDomainEventHandler<IncidentProcessedEvent>
	{
		private readonly ILogger<IncidentEventHandler> _logger;

		public IncidentEventHandler(ILogger<IncidentEventHandler> logger)
		{
			_logger = logger;
		}

		public Task HandleAsync(IncidentCreatedEvent @event)
		{
			_logger.LogInformation($"Incident created: {@event.Incident.Id} at {@event.OccurredOn}");
			return Task.CompletedTask;
		}

		public Task HandleAsync(IncidentUpdatedEvent @event)
		{
			_logger.LogInformation($"Incident updated: {@event.Incident.Id} at {@event.OccurredOn}");
			return Task.CompletedTask;
		}

		public Task HandleAsync(IncidentProcessedEvent @event)
		{
			_logger.LogInformation($"Incident processed: {@event.Incident.Id} at {@event.OccurredOn}");
			return Task.CompletedTask;
		}
	}
}
