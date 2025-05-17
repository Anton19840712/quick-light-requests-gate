using domain.events;
using quick_light_requests_gate.tmp;

public class EventPublisher : IEventPublisher
{
	private readonly IDomainEventDispatcher _dispatcher;
	private readonly ILogger<EventPublisher> _logger;
	public EventPublisher(
		IDomainEventDispatcher dispatcher,
		ILogger<EventPublisher> logger)
	{
		_dispatcher = dispatcher;
		_logger = logger;
	}
	public async Task PublishAsync<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
	{
		try
		{
			await _dispatcher.DispatchAsync(new IDomainEvent[] { domainEvent });
			_logger.LogInformation($"Event {typeof(TEvent).Name} published successfully");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, $"Error publishing event {typeof(TEvent).Name}");
			throw;
		}
	}
	public async Task PublishAsync(IEnumerable<IDomainEvent> events)
	{
		try
		{
			await _dispatcher.DispatchAsync(events);
			_logger.LogInformation($"Batch of {events.Count()} events published successfully");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error publishing events batch");
			throw;
		}
	}
}