using System;
using Anshan.Domain;

namespace Anshan.Persistence.Outbox
{
    public class OutboxItem : Entity<int>
    {
        public OutboxItem()
        {
            EventId = Guid.NewGuid();
            PublishDateTime = DateTime.Now;
        }

        public Guid EventId { get; set; }

        public string EventType { get; set; }

        public string EventBody { get; set; }

        public DateTime? PublishDateTime { get; set; }
    }
}