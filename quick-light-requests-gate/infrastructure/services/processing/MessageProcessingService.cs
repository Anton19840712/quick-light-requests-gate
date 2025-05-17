using application.interfaces.persistence;
using application.interfaces.services;
using domain.entities;
using domain.enums;
using domain.models.outbox;
using quick_light_requests_gate.tmp;

namespace infrastructure.services.processing
{
	// Сервис обработки сообщений:
	public class MessageProcessingService : IMessageProcessingService
	{
		private readonly IDomainEventDispatcher _domainEventDispatcher;
		private readonly IMongoRepository<OutboxMessage> _outboxRepository;
		private readonly IMongoRepository<IncidentEntity> _incidentRepository;
		private readonly ILogger<MessageProcessingService> _logger;

		public MessageProcessingService(
			IDomainEventDispatcher domainEventDispatcher,
			IMongoRepository<OutboxMessage> outboxRepository,
			IMongoRepository<IncidentEntity> incidentRepository,
			ILogger<MessageProcessingService> logger)
		{
			_domainEventDispatcher = domainEventDispatcher;
			_outboxRepository = outboxRepository ?? throw new ArgumentNullException(nameof(outboxRepository));
			_incidentRepository = incidentRepository ?? throw new ArgumentNullException(nameof(incidentRepository));
			_logger = logger;
		}

		public async Task ProcessIncomingMessageAsync(
			string message,
			string instanceModelQueueOutName,
			string instanceModelQueueInName,
			string host,
			int? port,
			string protocol)
		{
			try
			{
				var outboxMessage = new OutboxMessage
				{
					Id = Guid.NewGuid().ToString(),
					ModelType = ModelType.Outbox,
					EventType = EventTypes.Received,
					IsProcessed = false,
					ProcessedAt = DateTime.Now,
					InQueue = instanceModelQueueInName,
					OutQueue = instanceModelQueueOutName,
					Payload = message,
					RoutingKey = $"routing_key_{protocol}",
					CreatedAt = DateTime.UtcNow,
					Source = $"{protocol}-server-instance based on host: {host} and port {port}"
				};
				await _outboxRepository.SaveMessageAsync(outboxMessage);

				var incidentEntity = new IncidentEntity(message)
				{
					CreatedAtUtc = DateTime.UtcNow,
					CreatedBy = $"{protocol}-server-instance",
					IpAddress = "default",
					UserAgent = $"{protocol}-server-instance",
					CorrelationId = Guid.NewGuid().ToString(),
					ModelType = "Incident",
					IsProcessed = false
				};

				await _incidentRepository.SaveMessageAsync(incidentEntity);

				await _domainEventDispatcher.DispatchAsync(incidentEntity.DomainEvents);

				incidentEntity.ClearDomainEvents();
			}
			catch (Exception ex)
			{
				_logger.LogError("Ошибка при обработке сообщения.");
				_logger.LogError(ex.Message);
			}
		}
	}
}
