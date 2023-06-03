using System;

namespace EventSourcingDemo.Combat
{
    public partial class Character
    {
        #region Public Interface

        /// <summary>Adds the supplied attributes delta to the current <see cref="Attributes" />.</summary>
        /// <param name="delta"></param>
        /// <returns>Either an <see cref="AttributesModified" /> event or an <see cref="Error" />.</returns>
        public Result<AttributesModified> Add(Attributes delta) => new AttributesModified(StreamId, delta);

        #endregion
    }

    public record ModifyAttributes : Command
    {
        #region Creation

        public ModifyAttributes(Guid id, StreamId streamId) : base(id, streamId)
        {
        }

        public ModifyAttributes(StreamId streamId) : base(streamId)
        {
        }

        public ModifyAttributes(string category) : base(category)
        {
        }

        #endregion

        #region Public Interface

        public Attributes Delta { get; init; }

        #endregion
    }

    public record AttributesModified : Event
    {
        #region Creation

        public AttributesModified(StreamId streamId, Attributes delta) : base(streamId)
        {
            Delta = delta;
        }

        #endregion

        #region Public Interface

        public Attributes Delta { get; init; }

        public void Deconstruct(out Attributes delta)
        {
            delta = Delta;
        }

        #endregion
    }
}
