using System.Collections;
using System.Collections.Generic;

namespace EventSourcingDemo.Combat
{
    public abstract class Stream : IEnumerable<Event>
    {
        protected readonly SortedSet<Event> Events = new();

        #region Creation

        protected Stream(params Event[] events)
        {
            foreach (var e in events)
                Events.Add(e);
        }

        #endregion

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region IEnumerable<Event> Implementation

        public IEnumerator<Event> GetEnumerator() => Events.GetEnumerator();

        #endregion
    }
}
