using System;

namespace EventSourcingDemo.Combat
{
    public abstract record Message
    {
        #region Creation

        protected Message(Guid entityId)
        {
            EntityId = entityId;
        }

        #endregion

        #region Implementation

        public Guid Id { get; init; } = Guid.NewGuid();
        public Guid EntityId { get; }
        public string Category { get; init; }
        public Metadata Metadata { get; init; }
        public Version Version { get; set; }

        #endregion
    }
}
