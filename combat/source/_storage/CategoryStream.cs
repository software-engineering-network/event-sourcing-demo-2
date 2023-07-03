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
            Id = new CategoryStreamId(events[0].StreamId.Category);
        }

        public static Result<CategoryStream> From(params Event[] events) =>
            events.Select(x => x.StreamId.Category).Distinct().Count() > 1
                ? EventsFromDifferentCategories()
                : new CategoryStream(events);

        #endregion

        #region Implementation

        public CategoryStreamId Id { get; }

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
