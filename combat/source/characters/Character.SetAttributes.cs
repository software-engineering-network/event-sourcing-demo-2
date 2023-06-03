using System;

namespace EventSourcingDemo.Combat
{
    public partial class Character
    {
        #region Creation

        private Character(Character source, Attributes attributes) : this(source)
        {
            Attributes = attributes;
        }

        #endregion

        #region Public Interface

        /// <summary>Replaces the current <see cref="Attributes" />.</summary>
        /// <param name="attributes"></param>
        /// <returns>Either an <see cref="AttributesSet" /> event or an <see cref="Error" />.</returns>
        public Result<AttributesSet> Set(Attributes attributes)
        {
            if (attributes == Attributes)
                return NoOp("No operation necessary.");

            return new AttributesSet(StreamId, attributes);
        }

        #endregion

        #region Static Interface

        /// <summary>
        ///     Reducer. Takes the current <see cref="Character" /> instance and applies the supplied
        ///     <see cref="AttributesSet" /> event.
        /// </summary>
        /// <param name="event"></param>
        /// <returns>A new <see cref="Character" /> with the <see cref="AttributesSet" /> event applied.</returns>
        private static Character Apply(Character target, AttributesSet @event) => new(target, @event.Attributes);

        #endregion
    }

    public record AttributesSet : Event
    {
        #region Creation

        public AttributesSet(StreamId streamId, Attributes attributes) : base(streamId)
        {
            Attributes = attributes;
        }

        #endregion

        #region Public Interface

        public Attributes Attributes { get; init; }

        public void Deconstruct(out Attributes attributes)
        {
            attributes = Attributes;
        }

        #endregion
    }

    public record SetAttributes : Command
    {
        #region Creation

        public SetAttributes(Guid entityId, Attributes attributes) : base(
            new StreamId(CharacterManagementService.Category, entityId)
        )
        {
            Attributes = attributes;
        }

        #endregion

        #region Public Interface

        public Attributes Attributes { get; init; }

        public void Deconstruct(
            out StreamId entityStreamId,
            out Attributes attributes
        )
        {
            entityStreamId = GetEntityStreamId();
            attributes = Attributes;
        }

        #endregion
    }
}
