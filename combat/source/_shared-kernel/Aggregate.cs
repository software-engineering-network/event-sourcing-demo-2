using System;
using System.Collections.Generic;

namespace EventSourcingDemo.Combat
{
    public abstract class Aggregate : Entity
    {
        private readonly List<Event> _events = new();
        private readonly Dictionary<Type, Action<Event>> _handlers = new();

        #region Creation

        protected Aggregate(Guid id = default) : base(id)
        {
        }

        #endregion

        #region Public Interface

        public IReadOnlyList<Event> Events => _events.AsReadOnly();

        public void Add(Event e)
        {
            _events.Add(e);
        }

        public void Register<T>(Action<Event> handler)
        {
            _handlers[typeof(T)] = handler;
        }

        public void Replay(params Event[] events)
        {
        }

        #endregion
    }
}
