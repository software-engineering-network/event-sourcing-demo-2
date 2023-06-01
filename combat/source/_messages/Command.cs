using System;

namespace EventSourcingDemo.Combat
{
    public record Command : Message
    {
        #region Creation

        public Command(Guid id, StreamId streamId) : base(id, streamId)
        {
        }

        public Command(StreamId streamId) : base(streamId)
        {
        }

        public Command(string category) : base(new StreamId(category, Guid.NewGuid()))
        {
        }

        #endregion

        #region Public Interface

        public StreamId GetEntityStreamId() => new(Category, EntityId);

        #endregion

        public interface IHandler<in T>
        {
            public Result Handle(T command);
        }
    }
}
