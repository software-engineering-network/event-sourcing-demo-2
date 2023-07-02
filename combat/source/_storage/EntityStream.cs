using System.Linq;

namespace EventSourcingDemo.Combat
{
    public class EntityStream : Stream
    {
        private static readonly EntityStream EmptyStream = new();

        #region Creation

        private EntityStream(params Event[] events) : base(events)
        {
        }

        public static Result<EntityStream> From(params Event[] events) =>
            events.Select(x => x.StreamId).Distinct().Count() != 1
                ? EventsFromMoreThanOneEntity()
                : new EntityStream(events);

        #endregion

        #region Static Interface

        public static Error EventsFromMoreThanOneEntity() => new("inconsistent.stream");

        public static implicit operator EntityStream(Event[] events) => From(events).OnError(() => EmptyStream);

        #endregion
    }
}
