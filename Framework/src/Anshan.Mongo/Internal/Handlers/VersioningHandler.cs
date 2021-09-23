using System.Threading;
using System.Threading.Tasks;
using Anshan.Domain;
using ChainRunner;

namespace Anshan.Mongo.Internal.Handlers
{
    public class VersioningHandler<TAggregateRoot> : IResponsibilityHandler<TAggregateRoot>
    {
        public Task HandleAsync(TAggregateRoot aggregateRoot,
                                IChainContext chainContext,
                                CancellationToken cancellationToken = new())
        {
            // 1
            if (aggregateRoot is IVersionable versionableAggregateRoot)
            {
                versionableAggregateRoot.IncrementVersion();
                // 2
            }
            
            return Task.CompletedTask;
        }
    }
}