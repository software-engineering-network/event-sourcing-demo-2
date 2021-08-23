using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;

namespace EventSourcingDemo.CombatSpec
{
    public class SetAttributesHandlerSpec
    {
        #region Core

        private readonly Character _character;
        private readonly ICharacterRepository _characterRepository;

        public SetAttributesHandlerSpec()
        {
            _characterRepository = new CharacterRepository();
            _character = new Character("Mario");
            _characterRepository.Create(_character);
        }

        #endregion

        #region Test Methods

        [Fact]
        public void WhenSettingAttributes()
        {
            var command = new SetAttributes(_character.Id, 20, 0, 20, 10, 2, 20);
            var handler = new SetAttributes.Handler(_characterRepository);

            var result = handler.Handle(command);

            result.Status.Should().Be(Status.Succeeded);
        }

        #endregion
    }
}
