
using domain.entities;

namespace domain.events
{
    public class IncidentUpdatedEvent : DomainEventBase
    {
        public IncidentEntity Incident { get; }
        
        public IncidentUpdatedEvent(IncidentEntity incident) : base()
        {
            Incident = incident;
        }
    }
}
