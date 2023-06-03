using System;
using System.Collections.Generic;
using static EventSourcingDemo.Combat.CharacterManagementService;

namespace EventSourcingDemo.Combat
{
    public partial class Character : Aggregate
    {
        private static readonly Dictionary<Type, Func<Character, Event, Character>> Handlers = new();

        #region Creation

        static Character()
        {
            Register<AttributesModified>(Apply);
            Register<AttributesSet>(Apply);
            Register<CharacterCreated>(Apply);
            Register<CharacterRenamed>(Apply);
        }

        private Character(Guid id, Attributes attributes, string name) : base(id)
        {
            Attributes = attributes;
            Name = name;
        }

        private Character(Character source) : this(
            source.Id,
            source.Attributes,
            source.Name
        )
        {
        }

        #endregion

        #region Public Interface

        public Attributes Attributes { get; }
        public string Name { get; }

        #endregion

        #region Private Interface

        private StreamId StreamId => new(Category, Id);

        #endregion

        #region Static Interface

        private static void Register<T>(Func<Character, T, Character> handler) where T : Event
        {
            Handlers.Add(typeof(T), (character, @event) => handler(character, (T)@event));
        }

        #endregion
    }
}
