namespace EventSourcingDemo.Combat
{
    public partial class Character
    {
        #region Public Interface

        /// <summary>Adds the supplied attributes delta to the current <see cref="Attributes" />.</summary>
        /// <param name="delta"></param>
        /// <returns>Either an <see cref="AttributesModified" /> event or an <see cref="Error" />.</returns>
        public Result<AttributesModified> Add(Attributes delta)
        {
            if (delta == Attributes.Default)
                return NoOp();

            return new AttributesModified(delta);
        }

        #endregion

        #region Static Interface

        public static Character Apply(Character target, AttributesModified @event) =>
            new(target, target.Attributes + @event.Delta);

        #endregion
    }

    public record ModifyAttributes(Attributes Delta) : Command;

    public record AttributesModified(Attributes Delta) : Event;
}
