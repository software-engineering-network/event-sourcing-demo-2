using System;
using System.Collections.Generic;
using static EventSourcingDemo.Combat.Attributes;

namespace EventSourcingDemo.Combat
{
    public class Character : Aggregate
    {
        private readonly EventProcessor _eventProcessor = new();

        #region Creation

        public Character(
            string name,
            Attributes attributes = default
        ) : this(Guid.Empty)
        {
            _eventProcessor.Add(
                new CharacterCreated(
                    attributes ?? Default,
                    Id,
                    name
                )
            );
        }

        public Character(Guid id, params IEvent[] events) : this(id)
        {
            _eventProcessor.Replay(events);
        }

        private Character(Guid id) : base(id)
        {
            _eventProcessor
                .Register<CharacterCreated>(Handler)
                .Register<AttributesSet>(Handler)
                .Register<AttributesModified>(Handler)
                .Register<Renamed>(Handler);
        }

        #endregion

        #region Public Interface

        public Attributes Attributes { get; private set; }
        public IReadOnlyCollection<IEvent> Events => _eventProcessor.Events;
        public string Name { get; private set; }

        public Character ModifyAttributes(Attributes attributes)
        {
            _eventProcessor.Add(new AttributesModified(attributes, Id));
            return this;
        }

        public Character Rename(string name)
        {
            _eventProcessor.Add(new Renamed(name));
            return this;
        }

        public Character SetAttributes(Attributes attributes)
        {
            _eventProcessor.Add(new AttributesSet(attributes, Id));
            return this;
        }

        #endregion

        #region Private Interface

        private void Handler(AttributesModified e)
        {
            Attributes += e.Attributes;
        }

        private void Handler(AttributesSet e)
        {
            Attributes = e.Attributes;
        }

        private void Handler(CharacterCreated e)
        {
            Name = e.Name;
            Attributes = e.Attributes;
        }

        private void Handler(Renamed e)
        {
            Name = e.Name;
        }

        #endregion
    }
}
