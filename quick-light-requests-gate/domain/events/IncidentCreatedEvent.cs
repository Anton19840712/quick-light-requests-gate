
using domain.entities;

namespace domain.events
{
    public class IncidentCreatedEvent : DomainEventBase
    {
        public IncidentEntity Incident { get; }
        
        public IncidentCreatedEvent(IncidentEntity incident) : base()
        {
            Incident = incident;
        }
    }
}
