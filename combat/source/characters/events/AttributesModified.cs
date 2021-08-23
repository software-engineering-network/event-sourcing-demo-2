using System;

namespace EventSourcingDemo.Combat
{
    public class AttributesModified : Event
    {
        #region Creation

        public AttributesModified(
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
