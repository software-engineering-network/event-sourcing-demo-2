using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;
using static EventSourcingDemo.Combat.Character;
using static EventSourcingDemo.Combat.CharacterManagementService;
using static EventSourcingDemo.CombatSpec.ObjectProvider;

namespace EventSourcingDemo.CombatSpec.CharacterManagementServiceSpec
{
    public class WhenCreatingACharacter
    {
        #region Core

        private readonly CreateCharacter _command;
        private readonly CharacterCreated _event;

        public WhenCreatingACharacter()
        {
            var service = CreateService(out var store);
            service.Handle(_command = new CreateCharacter(Attributes.Mario, "Mario"));

            var stream = store.Find(new StreamId(Category, _command.EntityId)).Value;

            _event = (CharacterCreated)stream[0];
        }

        #endregion

        #region Test Methods

        [Fact]
        public void ThenCategoryIsCharacterManagement() => _event.Category.Should().Be(Category);

        [Fact]
        public void ThenEntityIdMatchesCommandEntityId() => _event.EntityId.Should().Be(_command.EntityId);

        #endregion

        public class GivenAConflictingCharacter
        {
            #region Test Methods

            [Fact]
            public void ThenReturnCharacterAlreadyExistsError()
            {
                var service = CreateService();
                service.Handle(new CreateCharacter(Attributes.Mario, "Mario"));

                var error = service.Handle(new CreateCharacter(Attributes.Mario, "Mario")).Error;

                error.Should().Be(CharacterAlreadyExists());
            }

            #endregion
        }
    }
}
