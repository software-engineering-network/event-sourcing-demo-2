using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingDemo.Combat
{
    public class CategoryStream : Stream
    {
        #region Creation

        private CategoryStream(params Event[] events) : base(events)
        {
        }

        public static Result<CategoryStream> From(params Event[] events)
        {
            if (events.Length == 0)
                return EmptyStream();

            return events.Select(x => x.StreamId.Category).Distinct().Count() == 1
                ? new CategoryStream(events)
                : EventsFromDifferentCategories();
        }

        #endregion

        #region Implementation

        public CategoryStreamId Id { get; private set; }

        public IEnumerable<EntityStreamId> GetEntityStreamIds() =>
            Events.Select(x => new EntityStreamId(Id.Category, x.EntityId)).Distinct();

        public HashSet<T> Project<T>(Func<Event[], Result<T>> rehydrate) =>
            GetEntityStreamIds()
                .Select(id => rehydrate(Events.Where(x => x.IsInEntity(id)).ToArray()).Value)
                .ToHashSet();

        #endregion

        #region Static Interface

        public static Error EventsFromDifferentCategories() => new("different-categories");

        #endregion
    }
}
