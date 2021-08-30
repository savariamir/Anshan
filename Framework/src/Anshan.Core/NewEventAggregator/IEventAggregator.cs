using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Anshan.Core.NewEventAggregator
{
    public interface IEventAggregator : IDisposable
    {
        IDisposable Subscribe<T>(Action<T> action); //where T : IDomainEvent;

        void Publish<T>(T @event); //where T : IDomainEvent;
    }

    public class EventAggregator : IEventAggregator
    {
        private readonly Subject<object> _subject = new();

        public IDisposable Subscribe<T>(Action<T> action)
        {
            return _subject.OfType<T>()
                           .AsObservable()
                           .Subscribe(action);
        }

        public void Publish<T>(T sampleEvent)
        {
            _subject.OnNext(sampleEvent);
        }

        public void Dispose()
        {
            _subject.Dispose();
        }
    }
}