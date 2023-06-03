using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;
using static EventSourcingDemo.Combat.Character;
using static EventSourcingDemo.Combat.CharacterManagementService;

namespace EventSourcingDemo.CombatSpec.CharacterSpec
{
    public class WhenCreating
    {
        #region Test Methods

        [Fact]
        public void ThenEntityIdIsCommandEntityId()
        {
            var streamId = new StreamId(Category);
            var character = From(new CharacterCreated(streamId, "Mario")).Value;

            character.Id.Should().Be(streamId.EntityId);
        }

        #endregion
    }

    public class WhenRenaming
    {
        #region Test Methods

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
