using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;
using static EventSourcingDemo.Combat.CreateCharacter;

namespace EventSourcingDemo.CombatSpec
{
    public class CreateCharacterHandlerSpec
    {
        #region Core

        private readonly ICharacterRepository _characterRepository;
        private readonly Handler _handler;

        public CreateCharacterHandlerSpec()
        {
            _characterRepository = new CharacterRepository();
            _handler = new Handler(_characterRepository);
        }

        #endregion

        #region Test Methods

        [Fact]
        public void WhenCreating()
        {
            var result = _handler.Handle(new CreateCharacter("Mario"));

            var character = _characterRepository.Find(result.Id);

            result.Status.Should().Be(Status.Succeeded);
            character.Should().NotBeNull();
        }

        #endregion
    }
}
