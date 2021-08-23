using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;

namespace EventSourcingDemo.CombatSpec
{
    public class CreateCharacterHandlerSpec
    {
        #region Core

        private readonly ICharacterRepository _characterRepository;

        public CreateCharacterHandlerSpec()
        {
            _characterRepository = new CharacterRepository();
        }

        #endregion

        #region Test Methods

        [Fact]
        public void WhenCreating()
        {
            var command = new CreateCharacter("Mario");
            var handler = new CreateCharacter.Handler(_characterRepository);

            var result = handler.Handle(command);

            result.Status.Should().Be(Status.Succeeded);
        }

        #endregion
    }
}
