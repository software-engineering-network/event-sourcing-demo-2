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

        public static Result<Character> Rehydrate(EntityStream stream)
        {
            if (stream.First().GetType() != typeof(CharacterCreated))
                return StreamError("could not rehydrate");

            Character character = null;

            using var events = stream.GetEnumerator();

            while (events.MoveNext())
                character = Handlers[events.Current.GetType()](character, events.Current);

            return character;
        }

        #endregion

        #region Static Interface

        private static Character Apply(Character target, CharacterCreated @event) => new(@event);

        public static Error CharacterAlreadyExists(string message = "") =>
            new($"{nameof(Character)}.{nameof(CharacterAlreadyExists)}", message);

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

        public void Deconstruct(out Attributes Attributes, out string Name)
        {
            Attributes = this.Attributes;
            Name = this.Name;
        }

        #endregion

        #region Implementation

        public Attributes Attributes { get; init; }
        public string Name { get; init; }

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

        public void Deconstruct(out Attributes Attributes, out string Name)
        {
            Attributes = this.Attributes;
            Name = this.Name;
        }

        #endregion

        #region Implementation

        public Attributes Attributes { get; init; }
        public string Name { get; init; }

        #endregion
    }
}
