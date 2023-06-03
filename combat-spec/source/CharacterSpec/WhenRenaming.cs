using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;
using static EventSourcingDemo.Combat.Character;
using static EventSourcingDemo.CombatSpec.ObjectProvider;

namespace EventSourcingDemo.CombatSpec.CharacterSpec
{
    public class WhenRenaming
    {
        #region Test Methods

        [Fact]
        public void ThenReducerRenamesCharacter()
        {
            var character = From(
                GetCharacterCreated(),
                new CharacterRenamed("Maria")
            ).Value;

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
