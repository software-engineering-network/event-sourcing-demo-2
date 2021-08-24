using System;
using System.Collections.Generic;

namespace EventSourcingDemo.Combat
{
    public class EventProcessor
    {
        private readonly List<IEvent> _events = new();
        private readonly Dictionary<Type, Action<IEvent>> _handlers = new();

        #region Public Interface

        public IReadOnlyCollection<IEvent> Events => _events.AsReadOnly();

        public EventProcessor Add(IEvent e)
        {
            _handlers[e.GetType()].Invoke(e);
            _events.Add(e);

            return this;
        }

        public EventProcessor Register<T>(Action<T> handler) where T : IEvent
        {
            _handlers.Add(typeof(T), e => handler((T) e));
            return this;
        }

        public EventProcessor Replay(params IEvent[] events)
        {
            foreach (var e in events)
                _handlers[e.GetType()].Invoke(e);

            return this;
        }

        #endregion
    }
}
