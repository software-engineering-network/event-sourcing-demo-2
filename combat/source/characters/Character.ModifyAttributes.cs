namespace EventSourcingDemo.Combat
{
    public partial class Character
    {
        #region Public Interface

        /// <summary>Adds the supplied attributes delta to the current <see cref="Attributes" />.</summary>
        /// <param name="delta"></param>
        /// <returns>Either an <see cref="AttributesModified" /> event or an <see cref="Error" />.</returns>
        public Result<AttributesModified> Add(Attributes delta) => new AttributesModified(delta);

        #endregion
    }

    public record ModifyAttributes(Attributes Delta) : Command;

    public record AttributesModified(Attributes Delta) : Event;
}
