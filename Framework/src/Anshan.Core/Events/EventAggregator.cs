using System.Collections.Generic;
using System.Linq;

namespace Anshan.Core.Events
{
    public class EventAggregator : IEventPublisher, IEventListener
    {
        private readonly List<object> _handlers = new();

        public void Subscribe<T>(IEventHandler<T> handler) where T : IEvent
        {
            _handlers.Add(handler);
        }

        public void Publish<T>(T @event) where T : IEvent
        {
            var handlers = _handlers.OfType<IEventHandler<T>>().ToList();
            handlers.ForEach(p => p.HandleAsync(@event));
        }
    }
}