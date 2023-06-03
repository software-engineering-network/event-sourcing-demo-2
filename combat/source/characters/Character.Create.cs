using System;
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

        public CreateCharacter(Attributes attributes, string name)
        {
            Category = CharacterManagementService.Category;
            EntityId = Guid.NewGuid();
            Attributes = attributes;
            Name = name;
        }

        #endregion

        #region Public Interface

        public Attributes Attributes { get; init; }
        public string Name { get; init; }

        public void Deconstruct(out Attributes Attributes, out string Name)
        {
            Attributes = this.Attributes;
            Name = this.Name;
        }

        #endregion
    }

    public record CharacterCreated : Event
    {
        #region Creation

        public CharacterCreated(Guid id, Attributes attributes, string name)
        {
            Id = id;
            Attributes = attributes;
            Name = name;
        }

        #endregion

        #region Public Interface

        public Attributes Attributes { get; init; }
        public string Name { get; init; }

        public void Deconstruct(out Attributes Attributes, out string Name)
        {
            Attributes = this.Attributes;
            Name = this.Name;
        }

        #endregion
    }
}
