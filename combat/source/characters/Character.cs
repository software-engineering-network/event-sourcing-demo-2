using static EventSourcingDemo.Combat.CharacterManagementService;

namespace EventSourcingDemo.Combat
{
    public partial class Character : Aggregate
    {
        #region Public Interface

        public Attributes Attributes { get; }
        public string Name { get; }

        /// <summary>Replaces a <see cref="Character" />'s <see cref="Attributes" />.</summary>
        /// <param name="attributes"></param>
        /// <returns>Either an <see cref="AttributesSet" /> event or an <see cref="Error" />.</returns>
        public Result<AttributesSet> Set(Attributes attributes) => new AttributesSet(StreamId, attributes);

        #endregion

        #region Private Interface

        private StreamId StreamId => new(Category, Id);

        #endregion
    }
}
