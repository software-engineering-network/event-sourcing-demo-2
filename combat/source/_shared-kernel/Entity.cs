using System;

namespace EventSourcingDemo.Combat
{
    public abstract class Entity
    {
        #region Creation

        protected Entity(Guid id)
        {
            Id = id;
        }

        #endregion

        #region Implementation

        public Guid Id { get; }

        #endregion
    }
}
