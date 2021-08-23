using System;

namespace EventSourcingDemo.Combat
{
    public class CharacterCreated : Event
    {
        #region Creation

        public CharacterCreated(
            Attributes attributes,
            Guid characterId,
            string name
        )
        {
            Attributes = attributes;
            CharacterId = characterId;
            Name = name;
        }

        #endregion

        #region Public Interface

        public Attributes Attributes { get; }
        public Guid CharacterId { get; }
        public string Name { get; }

        #endregion
    }
}
