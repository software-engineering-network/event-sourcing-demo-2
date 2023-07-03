using EventSourcingDemo.Combat;
using static EventSourcingDemo.Combat.Character;

namespace EventSourcingDemo.CombatSpec.Characters
{
    internal class ObjectProvider
    {
        #region Static Interface

        public static Character CreateCharacter() => Rehydrate(EntityStream.From(GetCharacterCreated())).Value;

        public static CharacterManager CreateService() => new(new MockEventStore());

        public static CharacterManager CreateService(out MockEventStore store) => new(store = new MockEventStore());

        public static CharacterCreated GetCharacterCreated() =>
            new CreateCharacter(
                new Attributes(20, 0, 20, 10, 2, 20),
                "Mario"
            ).ToEvent();

        #endregion
    }
}
