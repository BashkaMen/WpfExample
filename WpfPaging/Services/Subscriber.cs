using System;

namespace WpfPaging.Services
{
    class EventSubscriber : IDisposable
    {
        private readonly Action<EventSubscriber> _action;

        public Type MessageType { get; }

        public EventSubscriber(Type messageType, Action<EventSubscriber> action)
        {
            MessageType = messageType;
            _action = action;
        }

        public void Dispose()
        {
            _action?.Invoke(this);
        }
    }

    class MessageSubscriber : IDisposable
    {
        private readonly Action<MessageSubscriber> _action;

        public Type ReceiverType { get; }
        public Type MessageType { get; }

        public MessageSubscriber(Type receiverType, Type messageType, Action<MessageSubscriber> action)
        {
            ReceiverType = receiverType;
            MessageType = messageType;
            _action = action;
        }

        public void Dispose()
        {
            _action?.Invoke(this);
        }
    }


}
