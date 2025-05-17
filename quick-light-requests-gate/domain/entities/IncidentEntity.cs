
using MongoDB.Bson.Serialization.Attributes;
using domain.common;
using domain.events;

namespace domain.entities
{
    [BsonIgnoreExtraElements]
    public class IncidentEntity : AggregateRoot
    {
        public string Payload { get; private set; }

        public IncidentEntity(string payload)
        {
            Payload = payload;
            AddDomainEvent(new IncidentCreatedEvent(this));
        }

        public void UpdatePayload(string newPayload)
        {
            if (string.IsNullOrEmpty(newPayload))
                throw new DomainException("Payload cannot be empty");
            
            Payload = newPayload;
            AddDomainEvent(new IncidentUpdatedEvent(this));
        }
    }
}
