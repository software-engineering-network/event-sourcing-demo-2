using System;
using System.Collections.Generic;
using EventSourcingDemo.CombatSpec;

namespace EventSourcingDemo.Combat
{
    public class Character
    {
        private readonly List<Event> _events = new();

        #region Creation

        public Character(CharacterCreated e) : this(e.CharacterId)
        {
            Handler(e);
            _events.Add(e);
        }

        public Character(Guid id, params Event[] events) : this(id)
        {
            Replay(events);
        }

        protected Character(Guid id)
        {
            Id = id;
        }

        #endregion

        #region Public Interface

        public Attributes Attributes { get; private set; }
        public IReadOnlyList<Event> Events => _events.AsReadOnly();
        public Guid Id { get; }
        public string Name { get; private set; }

        public Character Replay(params Event[] events)
        {
            foreach (var e in events)
                if (e.Is(typeof(AttributesSet)))
                    Handler(e as AttributesSet);

            return this;
        }

        public Character SetAttributes(Attributes attributes)
        {
            var attributesSet = new AttributesSet(attributes, Id);

            _events.Add(attributesSet);

            /*
             * This seems to be aesthetic-only.
             * The final object won't be stored, only the event.
             * So really, this is for testing purposes.
             */
            return Handler(attributesSet);
        }

        #endregion

        #region Private Interface

        private Character Handler(AttributesSet e)
        {
            Attributes = e.Attributes;
            return this;
        }

        private Character Handler(CharacterCreated e)
        {
            Name = e.Name;
            return this;
        }

        #endregion

        #region Static Interface

        public static Character Create(string name)
        {
            var characterCreated = new CharacterCreated(Guid.NewGuid(), name);
            var character = new Character(characterCreated);
            return character;
        }

        #endregion
    }
}
