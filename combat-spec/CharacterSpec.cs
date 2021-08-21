using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;

namespace EventSourcingDemo.CombatSpec
{
    public class CharacterSpec
    {
        #region Test Methods

        [Fact]
        public void WhenCreatingCharacter()
        {
            var character = new Character();

            character.Id.Should().NotBeEmpty();
        }

        [Fact]
        public void WhenSettingAttributes()
        {
            var character = new Character();
            var attributes = new Attributes(20, 0, 20, 10, 2, 20);

            character.SetAttributes(attributes);

            character.Events.Should().Contain(x => x.Is(typeof(Character.AttributesSet)));
            character.Attributes.Should().Be(attributes);
        }

        #endregion
    }
}
