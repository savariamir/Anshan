using System;
using Anshan.Domain;

namespace Framework.Persistence.Outbox
{
    public class OutboxItem : Entity<string>
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