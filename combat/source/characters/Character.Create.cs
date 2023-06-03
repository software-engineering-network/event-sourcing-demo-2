using System.Linq;

namespace EventSourcingDemo.Combat
{
    public partial class Character
    {
        #region Creation

        private Character(CharacterCreated source) : this(
            source.EntityId,
            source.Attributes,
            source.Name
        )
        {
        }

        public static Result<Character> From(params Event[] stream)
        {
            if (stream[0].GetType() != typeof(CharacterCreated))
                return StreamError("could not rehydrate");

            var events = stream.ToList().GetEnumerator();

            Character character = null;

            while (events.MoveNext())
                character = Handlers[events.Current.GetType()](character, events.Current);

            return character;
        }

        #endregion

        #region Static Interface

        private static Character Apply(Character target, CharacterCreated @event) => new(@event);

        public static Error CannotDuplicateCharacter(string message = "") =>
            new($"{nameof(Character)}.{nameof(CannotDuplicateCharacter)}", message);

        #endregion
    }

    public record CreateCharacter : Command
    {
        #region Creation

        public CreateCharacter(string category, string name, Attributes? attributes) : base(category)
        {
            Name = name;
            Attributes = attributes;
        }

        #endregion

        #region Public Interface

        public Attributes? Attributes { get; set; }
        public string Name { get; init; }

        public void Deconstruct(out string name)
        {
            name = Name;
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
