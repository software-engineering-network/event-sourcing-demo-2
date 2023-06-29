using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingDemo.Combat
{
    public abstract class Stream
    {
        protected readonly SortedSet<Event> _events = new();

        #region Creation

        protected Stream(params Event[] events)
        {
            foreach (var e in events)
                _events.Add(e);
        }

        #endregion
    }

    public class EntityStream : Stream
    {
    }

    public class CategoryStream : Stream
    {
        #region Creation

        private CategoryStream(params Event[] events) : base(events)
        {
        }

        public static Result<CategoryStream> From(params Event[] events)
        {
            var stream = new CategoryStream(events);

            return stream;
        }

        #endregion

        #region Implementation

        public CategoryStreamId Id { get; private set; }

        public IEnumerable<EntityStreamId> GetEntityStreamIds() =>
            _events.Select(x => new EntityStreamId(Id.Category, x.EntityId)).Distinct();

        public HashSet<T> ProjectAll<T>(Func<Event[], Result<T>> rehydrate) =>
            GetEntityStreamIds()
                .Select(id => rehydrate(_events.Where(x => x.IsInEntity(id)).ToArray()).Value)
                .ToHashSet();

        #endregion
    }
}
