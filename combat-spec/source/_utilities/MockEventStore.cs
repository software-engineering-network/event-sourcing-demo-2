using System.Collections.Generic;
using System.Linq;
using EventSourcingDemo.Combat;

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

        public Result Push(Event @event)
        {
            _events.Add(@event);
            return Result.Success();
        }

        #endregion
    }
}
