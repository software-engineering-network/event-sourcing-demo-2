using System;

namespace EventSourcingDemo.Combat
{
    public abstract class Event : IEvent
    {
        #region Creation

        protected Event()
        {
            Id = Guid.NewGuid();
        }

        #endregion

        #region Public Interface

        public Guid Id { get; }

        #endregion
    }
}
