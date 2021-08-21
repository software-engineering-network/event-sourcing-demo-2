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
            var character = new Character();
            var command = new SetAttributes(character.Id, 20, 0, 20, 10, 2, 20);
            var handler = new SetAttributesHandler(new CharacterRepository());

            var result = handler.Handle(command);

            result.Status.Should().Be(Status.Succeeded);
        }

        #endregion
    }
}
