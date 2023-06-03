using System;
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
            var characterCreated = new CharacterCreated(
                Guid.NewGuid(),
                new Attributes(20, 0, 20, 10, 2, 20),
                "Mario"
            );
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
