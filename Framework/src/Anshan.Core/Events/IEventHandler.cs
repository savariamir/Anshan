using System.Threading;

namespace Anshan.Core.Events
{
    public interface IEventHandler<in T> where T : IEvent
    {
        void HandleAsync(T  @event, CancellationToken token = default);
    }
}