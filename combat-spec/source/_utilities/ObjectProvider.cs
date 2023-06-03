using System;
using EventSourcingDemo.Combat;
using static EventSourcingDemo.Combat.Character;

namespace EventSourcingDemo.CombatSpec
{
    internal class ObjectProvider
    {
        #region Static Interface

        public static Character CreateCharacter() => Rehydrate(GetCharacterCreated()).Value;

        public static CharacterManagementService CreateService() => new(new MockEventStore());

        public static CharacterManagementService CreateService(out MockEventStore store) =>
            new(store = new MockEventStore());

        public static CharacterCreated GetCharacterCreated() =>
            new(
                Guid.NewGuid(),
                new Attributes(20, 0, 20, 10, 2, 20),
                "Mario"
            );

        #endregion
    }
}
