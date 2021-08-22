using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;

namespace EventSourcingDemo.CombatSpec
{
    public class SetAttributesSpec
    {
        #region Core

        private readonly Character _character;
        private readonly ICharacterRepository _characterRepository;

        public SetAttributesSpec()
        {
            _characterRepository = new CharacterRepository();
            _character = Character.Create("Mario");
            _characterRepository.Save(_character);
        }

        #endregion

        #region Test Methods

        [Fact]
        public void WhenSettingAttributes()
        {
            var command = new SetAttributes(_character.Id, 20, 0, 20, 10, 2, 20);
            var handler = new SetAttributesHandler(_characterRepository);

            var result = handler.Handle(command);

            result.Status.Should().Be(Status.Succeeded);
        }

        #endregion
    }
}
