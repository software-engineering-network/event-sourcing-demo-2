﻿using System;
using System.Collections.Generic;
using EventSourcingDemo.CombatSpec;

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
        public IReadOnlyList<Event> Events => _events.AsReadOnly();
        public Guid Id { get; }

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
