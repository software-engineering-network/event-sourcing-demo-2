namespace EventSourcingDemo.Combat
{
    public partial class Character : Aggregate
    {
        #region Creation

        private Character(CharacterCreated source) : base(source.CharacterId)
        {
            Attributes = source.Attributes;
            Name = source.Name;
        }

        private Character(Character source) : base(source.Id)
        {
            var (attributes, name) = source;

            Attributes = attributes;
            Name = name;
        }

        private Character(Character source, Attributes attributes) : this(source)
        {
            Attributes = attributes;
        }

        public static Result<Character> From(params IEvent[] events)
        {
            var enumerator = events.GetEnumerator();

            var character = new Character((CharacterCreated)enumerator.Current);

            while (enumerator.MoveNext())
                character = character.Apply(enumerator.Current);

            return character;
        }

        #endregion

        #region Public Interface

        public Attributes Attributes { get; }
        public string Name { get; }

        /// <summary>
        ///     Reducer. Takes the current <see cref="Character" /> instance and applies the supplied
        ///     <see cref="AttributesModified" /> event.
        /// </summary>
        /// <param name="event"></param>
        /// <returns>A new <see cref="Character" /> with the <see cref="AttributesModified" /> event applied.</returns>
        public Result<Character> Apply(AttributesModified @event) => new Character(this, @event.Attributes);

        public void Deconstruct(out Attributes attributes, out string name)
        {
            attributes = Attributes;
            name = Name;
        }

        /// <summary>Replaces a <see cref="Character" />'s <see cref="Attributes" />.</summary>
        /// <remarks>
        ///     <para>Does the business logic. Can fail. Can make external calls. Can do whatever.</para>
        ///     <para>
        ///         This way, we can write unit tests against <see cref="Character" /> to guarantee it produces a particular
        ///         result.
        ///     </para>
        /// </remarks>
        /// <param name="attributes"></param>
        /// <returns>Either an <see cref="AttributesSet" /> event or an error.</returns>
        public Result<AttributesSet> Set(Attributes attributes) => new AttributesSet(attributes, Id);

        #endregion
    }
}
