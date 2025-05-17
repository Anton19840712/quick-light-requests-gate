using domain.events;
using quick_light_requests_gate.tmp;

public abstract class DomainEventHandler<TEvent> : IDomainEventHandler<TEvent>
	where TEvent : IDomainEvent
{
	protected readonly ILogger<DomainEventHandler<TEvent>> _logger;
	protected DomainEventHandler(ILogger<DomainEventHandler<TEvent>> logger)
	{
		_logger = logger;
	}
	public abstract Task HandleAsync(TEvent domainEvent);
}
