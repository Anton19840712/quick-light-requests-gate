using domain.events;

public interface IIncidentEventHandler
{
	Task HandleAsync(IncidentCreatedEvent @event);
	Task HandleAsync(IncidentUpdatedEvent @event);
	Task HandleAsync(IncidentProcessedEvent @event);
}
