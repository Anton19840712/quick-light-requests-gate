using domain.events;

namespace quick_light_requests_gate.tmp
{
	public interface IDomainEventHandler<TEvent> where TEvent : IDomainEvent
	{
		Task HandleAsync(TEvent domainEvent);
	}
}
