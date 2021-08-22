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
            var character = new Character("Mario");

            var storedCharacter = _characterRepository
                .Save(character)
                .Find(character.Id);

            storedCharacter.Id.Should().Be(character.Id);
        }

        #endregion
    }
}
