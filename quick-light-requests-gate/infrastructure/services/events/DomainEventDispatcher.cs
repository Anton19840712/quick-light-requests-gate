using domain.events;
using quick_light_requests_gate.tmp;

namespace infrastructure.services.events
{
	public class DomainEventDispatcher : IDomainEventDispatcher
	{
		private readonly IServiceProvider _serviceProvider;

		public DomainEventDispatcher(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents)
		{
			foreach (var domainEvent in domainEvents)
			{
				var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
				var handlers = _serviceProvider.GetServices(handlerType);

				foreach (var handler in handlers)
				{
					var method = handlerType.GetMethod("HandleAsync");
					await (Task)method.Invoke(handler, new object[] { domainEvent });
				}
			}
		}
	}
}
