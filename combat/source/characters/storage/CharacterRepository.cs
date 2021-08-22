using System;
using System.Collections.Generic;
using System.Linq;

namespace EventSourcingDemo.Combat
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly Dictionary<Guid, Event[]> _streams = new();

        #region ICharacterRepository Implementation

        public Character Find(Guid id)
        {
            if (!_streams.ContainsKey(id))
                throw new ArgumentException(
                    $"{nameof(Character)} not found.",
                    nameof(id)
                );

            return new Character(_streams[id]);
        }

        public ICharacterRepository Save(Character character)
        {
            _streams[character.Id] = character.Events.ToArray();
            return this;
        }

        #endregion
    }
}
