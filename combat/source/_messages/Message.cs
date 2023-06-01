using System;

namespace EventSourcingDemo.Combat
{
    public abstract record Message
    {
        #region Creation

        protected Message(Guid id, StreamId streamId)
        {
            Id = id;
            EntityId = streamId.EntityId;
            Category = streamId.Category;
        }

        protected Message(StreamId streamId) : this(
            Guid.NewGuid(),
            streamId
        )
        {
        }

        #endregion

        #region Public Interface

        public string Category { get; init; }
        public Guid EntityId { get; init; }
        public Guid Id { get; init; }

        #endregion
    }
}
