using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;
using static EventSourcingDemo.Combat.Character;

namespace EventSourcingDemo.CombatSpec.CharacterSpec
{
    public class WhenRenaming
    {
        #region Test Methods

        [Fact]
        public void ThenReducerRenamesCharacter()
        {
            var streamId = new StreamId(CharacterManagementService.Category);
            var characterCreated = new CharacterCreated(streamId, "Mario");
            var character = From(characterCreated).Value;
            var characterRenamed = character.Rename("Maria").Value;

            character = From(characterCreated, characterRenamed);

            character.Name.Should().Be(characterRenamed.Name);
        }

        [Fact]
        public void ThenReturnCharacterRenamedEvent()
        {
            var character = ObjectProvider.CreateCharacter();

            var @event = character.Rename("Maria").Value;

            @event.Name.Should().Be("Maria");
        }

        #endregion
    }
}
