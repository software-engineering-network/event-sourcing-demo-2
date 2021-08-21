using System;
using System.Collections.Generic;

namespace EventSourcingDemo.Combat
{
    public class Character
    {
        private readonly List<Event> _events = new();

        #region Creation

        public Character(Guid id = default)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
        }

        #endregion

        #region Public Interface

        public Attributes Attributes { get; private set; }
        public IReadOnlyCollection<Event> Events => _events.AsReadOnly();
        public Guid Id { get; }

        public Character SetAttributes(Attributes attributes)
        {
            var attributesSet = new AttributesSet(attributes, Id);

            _events.Add(attributesSet);

            /*
             * This appears to be aesthetic-only.
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

        #endregion

        public class AttributesSet : Event
        {
            #region Creation

            public AttributesSet(
                Attributes attributes,
                Guid characterId
            )
            {
                Attributes = attributes;
                CharacterId = characterId;
            }

            #endregion

            #region Public Interface

            public Attributes Attributes { get; }
            public Guid CharacterId { get; }

            #endregion
        }
    }
}
