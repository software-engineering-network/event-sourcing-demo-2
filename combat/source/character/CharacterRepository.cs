using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingDemo.Combat
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly Dictionary<Guid, Event[]> _streams = new();

        #region ICharacterRepository Implementation

        public Character Find(Guid id) =>
            _streams.ContainsKey(id)
                ? new Character().Replay(_streams[id])
                : null;

        public ICharacterRepository Save(Character character)
        {
            _streams[character.Id] = character.Events.ToArray();
            return this;
        }

        #endregion
    }
}
