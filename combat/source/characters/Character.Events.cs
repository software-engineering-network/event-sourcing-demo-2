namespace EventSourcingDemo.Combat
{
    public partial class Character
    {
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

        public record CharacterCreated : Event
        {
            #region Creation

            public CharacterCreated(
                StreamId streamId,
                string name,
                Attributes attributes = default
            ) : base(streamId)
            {
                Name = name;
                Attributes = attributes ?? Attributes.Default;
            }

            #endregion

            #region Public Interface

            public Attributes Attributes { get; init; }
            public string Name { get; init; }

            public void Deconstruct(
                out string name,
                out Attributes attributes
            )
            {
                attributes = Attributes;
                name = Name;
            }

            #endregion
        }
    }
}
