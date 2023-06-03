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

        #region Implementation

        /// <summary>Replaces the current <see cref="Attributes" />.</summary>
        /// <param name="attributes"></param>
        /// <returns>Either an <see cref="AttributesSet" /> event or an <see cref="Error" />.</returns>
        public Result<AttributesSet> Set(Attributes attributes)
        {
            if (attributes == Attributes)
                return NoOp("No operation necessary.");

            return new AttributesSet(attributes);
        }

        #endregion

        #region Static Interface

        private static Character Apply(Character target, AttributesSet @event) => new(target, @event.Attributes);

        #endregion
    }

    public record SetAttributes(Attributes Attributes) : Command;

    public record AttributesSet(Attributes Attributes) : Event;
}
