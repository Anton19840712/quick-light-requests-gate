using MongoDB.Bson.Serialization.Attributes;

namespace domain.entities
{
	[BsonIgnoreExtraElements]
	public class IncidentEntity : AuditableEntity
	{
		public string Payload { get; set; }
	}
}
