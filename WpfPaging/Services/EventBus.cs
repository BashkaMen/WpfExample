using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using WpfPaging.Events;

namespace WpfPaging.Services
{
    public class EventBus
    {
        private ConcurrentDictionary<EventSubscriber, Func<IEvent, Task>> _subscibers;

        public EventBus()
        {
            _subscibers = new ConcurrentDictionary<EventSubscriber, Func<IEvent, Task>>();
        }

        public IDisposable Subscribe<T>(Func<T, Task> handler) where T : IEvent
        {
            var disposer = new EventSubscriber(typeof(T), s => _subscibers.TryRemove(s, out var _));

            _subscibers.TryAdd(disposer, (item) => handler((T)item));

            return disposer;
        }

        public async Task Publish<T>(T message) where T : IEvent
        {
            var messageType = typeof(T);

            var handlers = _subscibers
                .Where(s => s.Key.MessageType == messageType)
                .Select(s => s.Value(message));

            await Task.WhenAll(handlers);
        }
    }
}
