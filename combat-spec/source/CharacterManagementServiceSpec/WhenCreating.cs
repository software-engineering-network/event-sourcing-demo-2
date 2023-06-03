using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;
using static EventSourcingDemo.Combat.Character;
using static EventSourcingDemo.Combat.CharacterManagementService;

namespace EventSourcingDemo.CombatSpec.CharacterManagementServiceSpec
{
    public class WhenCreating
    {
        #region Core

        private readonly CreateCharacter _command;
        private readonly CharacterCreated _event;

        public WhenCreating()
        {
            var store = new MockEventStore();
            var service = new CharacterManagementService(store);

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
                var store = new MockEventStore();
                var service = new CharacterManagementService(store);
                service.Handle(new CreateCharacter(Attributes.Mario, "Mario"));

                var error = service.Handle(new CreateCharacter(Attributes.Mario, "Mario")).Error;

                error.Should().Be(CharacterAlreadyExists());
            }

            #endregion
        }
    }
}
