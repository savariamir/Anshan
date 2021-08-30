using Anshan.Domain;

namespace Anshan.EF
{
    public class Audit : Entity<long>
    {
        public string AggregateRootName { get; set; }

        public string Values { get; set; }
    }
}