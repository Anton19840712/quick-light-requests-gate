using domain.entities;

namespace domain.events
{
    public class IncidentProcessedEvent : DomainEventBase
    {
        public IncidentEntity Incident { get; }
        
        public IncidentProcessedEvent(IncidentEntity incident) : base()
        {
            Incident = incident;
        }
    }
}
