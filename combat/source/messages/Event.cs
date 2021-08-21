using System;

namespace EventSourcingDemo.Combat
{
    public class Event
    {
        #region Creation

        public Event()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Public Interface

        public Guid Id { get; }

        #endregion
    }
}
