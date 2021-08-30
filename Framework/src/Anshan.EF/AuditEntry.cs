using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace Anshan.EF
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }

        public string TableName { get; set; }

        public Dictionary<string, object> Values { get; } = new();

        public Audit ToAudit()
        {
            var audit = new Audit
            {
                AggregateRootName = TableName,
                // CreatedAt = DateTime.Now,
                Values = Values.Count == 0 ? null : JsonConvert.SerializeObject(Values.Values)
            };
            return audit;
        }
    }
}