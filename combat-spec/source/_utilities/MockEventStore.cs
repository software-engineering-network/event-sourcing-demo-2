using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcingDemo.Combat;
using static EventSourcingDemo.Combat.Result;

namespace EventSourcingDemo.CombatSpec
{
    public class MockEventStore : IEventStore
    {
        private readonly List<Event> _events = new();

        #region IEventStore Implementation

        public Result<Event[]> Find(StreamId streamId)
        {
            return streamId.EntityId is null
                ? _events.Where(x => x.IsInCategory(streamId)).ToArray()
                : _events.Where(x => x.IsInEntity(streamId)).ToArray();
        }

        public Result<CategoryStream> Find(CategoryStreamId id)
        {
            var events = _events.Where(x => x.IsInCategory(id)).ToArray();
            return CategoryStream.From(id.Category, events);
        }

        public Result<EntityStream> Find(EntityStreamId id) => throw new NotImplementedException();

        public Result Push(Event @event)
        {
            _events.Add(@event);
            return Success();
        }

        #endregion
    }

    public class MockViewRepository : IViewRepository
    {
        private readonly Dictionary<string, View> _views = new();

        #region IViewRepository Implementation

        public Result Create(string name, View view)
        {
            _views.Add(name, view);
            return Success();
        }

        public Result<View> Find(string name) => _views[name];

        public Result Update(string name, View view)
        {
            _views[name] = view;
            return Success();
        }

        #endregion
    }
}
