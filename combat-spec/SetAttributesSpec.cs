using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;

namespace EventSourcingDemo.CombatSpec
{
    public class SetAttributesSpec
    {
        #region Test Methods

        [Fact]
        public void WhenSettingAttributes()
        {
            var characterRepository = new CharacterRepository();
            var character = new Character();
            characterRepository.Save(character);

            var command = new SetAttributes(character.Id, 20, 0, 20, 10, 2, 20);
            var handler = new SetAttributesHandler(characterRepository);

            var result = handler.Handle(command);

            result.Status.Should().Be(Status.Succeeded);
        }

        #endregion
    }
}
