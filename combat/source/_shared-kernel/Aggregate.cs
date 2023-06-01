using System;

namespace EventSourcingDemo.Combat
{
    public abstract class Aggregate : Entity
    {
        #region Creation

        protected Aggregate(Guid id) : base(id)
        {
        }

        #endregion
    }
}
