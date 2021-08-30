using System.Net;
using Anshan.Domain.Exceptions;

namespace Anshan.Query
{
    public class NotFoundResource : InfrastructureException
    {
        public NotFoundResource() : base("Resource Not Found", (int) HttpStatusCode.NotFound)
        {
        }
    }
}