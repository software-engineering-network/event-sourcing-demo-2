using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;
using static EventSourcingDemo.Combat.Character;
using static EventSourcingDemo.CombatSpec.Characters.ObjectProvider;

namespace EventSourcingDemo.CombatSpec.Characters.CharacterSpec
{
    public class WhenRenaming
    {
        #region Requirements

        [Fact]
        public void ThenReducerRenamesCharacter()
        {
            var character = CreateCharacter();

            character = Apply(character, new CharacterRenamed("Maria"));

            character.Name.Should().Be("Maria");
        }

        [Fact]
        public void ThenReturnCharacterRenamedEvent()
        {
            var character = CreateCharacter();

            var @event = character.Rename("Maria").Value;

            @event.Name.Should().Be("Maria");
        }

        #endregion
    }
}
