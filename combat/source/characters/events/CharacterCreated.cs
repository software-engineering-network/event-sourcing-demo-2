using System;

namespace EventSourcingDemo.Combat
{
    public class CharacterCreated : Event
    {
        #region Creation

        public CharacterCreated(Guid characterId, string name)
        {
            CharacterId = characterId;
            Name = name;
        }

        #endregion

        #region Public Interface

        public Guid CharacterId { get; }
        public string Name { get; }

        #endregion
    }
}
