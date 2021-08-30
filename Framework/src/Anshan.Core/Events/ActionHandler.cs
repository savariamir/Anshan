using System;
using System.Threading;

namespace Anshan.Core.Events
{
    public class ActionHandler<T> : IEventHandler<T> where T : IEvent
    {
        private readonly Action<T> _action;

        public ActionHandler(Action<T> action)
        {
            _action = action;
        }

        public void HandleAsync(T  eventToHandle, CancellationToken token = default)
        {
            _action(eventToHandle);
        }
    }
}