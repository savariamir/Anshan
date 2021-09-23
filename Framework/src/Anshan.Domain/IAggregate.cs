using System.Collections.Generic;

namespace Anshan.Domain
{
    public interface IAggregate
    {
        void Publish(DomainEvent domainEvent);

        IReadOnlyCollection<DomainEvent> GetEvents();

    }

    public interface IVersionable
    {
        int Version { get; }

        void IncrementVersion();
    }
}