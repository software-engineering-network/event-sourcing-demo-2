using EventSourcingDemo.Combat;
using static EventSourcingDemo.Combat.Character;

namespace EventSourcingDemo.CombatSpec
{
    internal class ObjectProvider
    {
        #region Static Interface

        public static Character CreateCharacter()
        {
            var streamId = new StreamId(CharacterManagementService.Category);

            return From(new CharacterCreated(streamId, "Mario")).Value;
        }

        #endregion
    }
}
