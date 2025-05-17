using domain.events;

namespace quick_light_requests_gate.tmp
{
	public interface IDomainEventDispatcher
	{
		Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents);
	}
}
