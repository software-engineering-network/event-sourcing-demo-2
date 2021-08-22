using System;

namespace EventSourcingDemo.Combat
{
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
