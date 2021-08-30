using System.Collections.Generic;
using System.Linq;
using Anshan.Domain;
using Newtonsoft.Json;

namespace Framework.Persistence.Outbox
{
    public static class OutboxItemFactory
    {
        public static IEnumerable<OutboxItem> CreateOutboxItem(IEnumerable<IDomainEvent> events)
        {
            return events.Select(CreateOutboxItem).ToList();
        }

        private static OutboxItem CreateOutboxItem(IDomainEvent @event)
        {
            return new()
            {
                EventId = @event.EventId,
                EventType = @event.GetType().Name,
                PublishDateTime = null,
                EventBody = JsonConvert.SerializeObject(@event)
            };
        }
    }
}