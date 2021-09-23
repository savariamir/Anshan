using System;
using Anshan.Scaffolder.Models;

namespace Anshan.Scaffolder
{
    public class DomainContext
    {
        public DomainContext(Type domainType)
        {
            DomainName = new MultiStyleText(domainType.Name);
            DomainType = domainType;
        }

        public Type DomainType { get; }

        public MultiStyleText DomainName { get; }
    }
}