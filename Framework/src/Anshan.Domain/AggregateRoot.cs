using System.Collections.Generic;

namespace Anshan.Domain
{
    public abstract class AggregateRoot<TKey> : Entity<TKey>, IAggregate
    {
        private List<DomainEvent> _domainEvents;

        protected AggregateRoot()
        {
            _domainEvents = new List<DomainEvent>();
        }

        public void Publish(DomainEvent domainEvent)
        {
            _domainEvents ??= new List<DomainEvent>();

            _domainEvents.Add(domainEvent);
        }

        public IReadOnlyCollection<DomainEvent> GetEvents()
        {
            return _domainEvents;
        }
    }
}