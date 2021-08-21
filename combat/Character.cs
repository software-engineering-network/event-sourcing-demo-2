using System;

namespace EventSourcingDemo.Combat
{
    public class Character
    {
        #region Creation

        public Character(Guid id = default)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
        }

        #endregion

        #region Public Interface

        public Attributes Attributes { get; private set; }
        public Guid Id { get; }

        public Character SetAttributes(Attributes attributes)
        {
            var attributesSet = new AttributesSet(attributes, Id);

            return Process(attributesSet);
        }

        #endregion

        #region Private Interface

        private Character Process(AttributesSet @event)
        {
            Attributes = @event.Attributes;
            return this;
        }

        #endregion

        public class AttributesSet
        {
            #region Creation

            public AttributesSet(
                Attributes attributes,
                Guid characterId
            )
            {
                Attributes = attributes;
                CharacterId = characterId;
                Id = Guid.NewGuid();
            }

            #endregion

            #region Public Interface

            public Attributes Attributes { get; }
            public Guid CharacterId { get; }
            public Guid Id { get; }

            #endregion
        }
    }
}
