using Anshan.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Anshan.EF
{
    public class Test : IEntityTypeConfiguration<Audit>
    {
        public void Configure(EntityTypeBuilder<Audit> builder)
        {
            builder.Property(p => p.Values);
        }
    }
    public class Audit : Entity<long>
    {
        public string AggregateRootName { get; set; }

        public string Values { get; set; }
    }
}