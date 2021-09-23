using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Anshan.Domain;
using ChainRunner;

namespace Anshan.Mongo.Internal.Handlers
{
    public class AuditingHandler<TAggregateRoot> : IResponsibilityHandler<TAggregateRoot>,
                                                   IResponsibilityHandler<IEnumerable<TAggregateRoot>>
    {
        private readonly bool _setCreationDate;

        public AuditingHandler(bool setCreationDate = false)
        {
            _setCreationDate = setCreationDate;
        }

        public Task HandleAsync(TAggregateRoot aggregateRoot,
                                IChainContext chainContext,
                                CancellationToken cancellationToken = new())
        {
            var now = DateTime.UtcNow;
            if (aggregateRoot is IAuditableEntity auditableEntity)
            {
                auditableEntity.SetModifiedAt(now);

                if (_setCreationDate)
                {
                    auditableEntity.SetCreatedAt(now);
                }
            }

            return Task.CompletedTask;
        }

        public async Task HandleAsync(IEnumerable<TAggregateRoot> request,
                                      IChainContext chainContext,
                                      CancellationToken cancellationToken = new())
        {
            foreach (var aggregateRoot in request)
            {
                await HandleAsync(aggregateRoot, chainContext, cancellationToken);
            }
        }
    }
}