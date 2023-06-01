using System;

namespace EventSourcingDemo.Combat
{
    public abstract record Event : Message
    {
        #region Creation

        protected Event(Guid id, StreamId streamId) : base(id, streamId)
        {
        }

        protected Event(StreamId streamId) : base(streamId)
        {
        }

        #endregion

        #region Public Interface

        public Event Apply(StreamId streamId)
        {
            var (category, entityId, _) = streamId;

            return this with
            {
                Category = category,
                EntityId = entityId
            };
        }

        #endregion
    }
}
