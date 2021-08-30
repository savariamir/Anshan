using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Anshan.Domain;
using Microsoft.EntityFrameworkCore;

namespace Anshan.EF
{
    public class CoreDbContext : DbContext
    {
        //public DbSet<OutboxMessage> OutboxMessages { get; set; }

        public CoreDbContext(DbContextOptions options) : base(options)
        {
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
                                                         CancellationToken cancellationToken = default)
        {
            OnBeforeSaving();

            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void OnBeforeSaving()
        {
            var entities = ChangeTracker.Entries()
                                        .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            var now = DateTime.UtcNow;
            var outboxMessages = new List<OutboxMessage>();

            foreach (var entry in entities)
            {
                if (!(entry.Entity is IAuditableEntity entity)) continue;

                //GenerateOutboxMessages(entry, outboxMessages);

                if (entry.State == EntityState.Added) entity.SetCreatedAt(now);

                entity.SetModifiedAt(now);
            }

            //this.OutboxMessages.AddRange(outboxMessages);
        }
    }
}