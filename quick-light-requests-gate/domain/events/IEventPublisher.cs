using domain.events;

public interface IEventPublisher
{
	Task PublishAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent;
	Task PublishAsync(IEnumerable<IDomainEvent> events);
}
