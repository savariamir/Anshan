using Anshan.Application.Exceptions;

namespace Anshan.Application
{
    public class AggregateNotFound : CustomApplicationException
    {
        public AggregateNotFound() : base("Entity Not Found")
        {
        }
    }
}