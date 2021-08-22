using System;
using EventSourcingDemo.CombatSpec;

namespace EventSourcingDemo.Combat
{
    public class Character : Aggregate
    {
        #region Creation

        public Character(CharacterCreated e) : base(e.CharacterId)
        {
            Handler(e);
            Add(e);
        }

        public Character(Guid id, params Event[] events) : base(id)
        {
            Replay(events);
        }

        #endregion

        #region Public Interface

        public Attributes Attributes { get; private set; }
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
            Handler(attributesSet);
            Add(attributesSet);
            return this;
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
