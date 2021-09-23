using System.Net;
using Anshan.Domain.Exceptions;

namespace Anshan.Application.Exceptions
{
    public class NotFoundResource : InfrastructureException
    {
        public NotFoundResource(string aggregateRootName, string id) :
            base($"Resource of type '{aggregateRootName}' with specified if of '{id}' not found", (int)HttpStatusCode.NotFound)
        {
        }
    }
}