using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingDemo.Combat
{
    public partial class Character
    {
        private static readonly Dictionary<Type, Func<Character, Event, Character>> Handlers = new();

        #region Creation

        static Character()
        {
            Register<CharacterCreated>(Apply);
            Register<CharacterRenamed>(Apply);
            Register<AttributesSet>(Apply);
        }

        private Character(Guid id, Attributes attributes, string name) : base(id)
        {
            Attributes = attributes;
            Name = name;
        }

        private Character(CharacterCreated source) : this(
            source.EntityId,
            source.Attributes,
            source.Name
        )
        {
        }

        private Character(Character source) : this(
            source.Id,
            source.Attributes,
            source.Name
        )
        {
        }

        private Character(Character source, Attributes attributes) : this(source)
        {
            Attributes = attributes;
        }

        private Character(Character source, string name) : this(source)
        {
            Name = name;
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

        #region Public Interface

        public void Deconstruct(out Attributes attributes, out string name)
        {
            attributes = Attributes;
            name = Name;
        }

        #endregion

        #region Static Interface

        private static Character Apply(Character target, CharacterCreated @event) => new(@event);

        /// <summary>
        ///     Reducer. Takes the current <see cref="Character" /> instance and applies the supplied
        ///     <see cref="AttributesSet" /> event.
        /// </summary>
        /// <param name="event"></param>
        /// <returns>A new <see cref="Character" /> with the <see cref="AttributesSet" /> event applied.</returns>
        private static Character Apply(Character target, AttributesSet @event) => new(target, @event.Attributes);

        private static Character Apply(Character target, CharacterRenamed @event) => new(target, @event.Name);

        private static void Register<T>(Func<Character, T, Character> handler) where T : Event
        {
            Handlers.Add(typeof(T), (character, @event) => handler(character, (T)@event));
        }

        #endregion
    }
}
