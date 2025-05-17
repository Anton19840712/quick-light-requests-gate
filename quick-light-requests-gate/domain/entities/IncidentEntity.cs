using MongoDB.Bson.Serialization.Attributes;
using domain.common;
using domain.events;
using domain.exceptions;

namespace domain.entities
{
    [BsonIgnoreExtraElements]
	public class IncidentEntity : AggregateRoot
	{
		public IncidentEntity(string payload)
		{
			if (string.IsNullOrEmpty(payload))
				throw new ArgumentException("Payload cannot be null or empty", nameof(payload));

			Payload = payload;
		}

		public string Payload { get; private set; }

		public void MarkAsProcessed()
		{
			IsProcessed = true;
			UpdatedAtUtc = DateTime.UtcNow;
			AddDomainEvent(new IncidentProcessedEvent(this));
		}

		public void UpdatePayload(string newPayload)
		{
			if (string.IsNullOrEmpty(newPayload))
				throw new DomainException("Payload cannot be empty");

			Payload = newPayload;
			UpdatedAtUtc = DateTime.UtcNow;
			AddDomainEvent(new IncidentUpdatedEvent(this));
		}
	}
}
