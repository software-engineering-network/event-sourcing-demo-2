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

            return Handler(attributesSet);
        }

        #endregion

        #region Private Interface

        private Character Handler(AttributesSet @event)
        {
            Attributes = @event.Attributes;
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
