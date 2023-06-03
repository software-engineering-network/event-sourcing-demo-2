namespace EventSourcingDemo.Combat
{
    public partial class Character
    {
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

        public record CharacterRenamed : Event
        {
            #region Creation

            public CharacterRenamed(StreamId streamId, string name) : base(streamId)
            {
                Name = name;
            }

            #endregion

            #region Public Interface

            public string Name { get; init; }

            #endregion
        }
    }
}
