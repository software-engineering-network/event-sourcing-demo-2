using System;
using EventSourcingDemo.Combat;
using static EventSourcingDemo.Combat.Character;

namespace EventSourcingDemo.CombatSpec
{
    internal class ObjectProvider
    {
        #region Static Interface

        public static Character CreateCharacter() =>
            From(
                new CharacterCreated(
                    Guid.NewGuid(),
                    new Attributes(20, 0, 20, 10, 2, 20),
                    "Mario"
                )
            ).Value;

        #endregion
    }
}
