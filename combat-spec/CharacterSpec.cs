using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;

namespace EventSourcingDemo.CombatSpec
{
    public class CharacterSpec
    {
        #region Core

        private readonly Character _character;

        public CharacterSpec()
        {
            _character = Character.Create("Mario");
        }

        #endregion

        #region Test Methods

        [Fact]
        public void WhenCreatingCharacter()
        {
            _character.Events.Should().Contain(x => x.Is(typeof(CharacterCreated)));
            _character.Id.Should().NotBeEmpty();
            _character.Name.Should().Be("Mario");
        }

        [Fact]
        public void WhenSettingAttributes()
        {
            var attributes = new Attributes(20, 0, 20, 10, 2, 20);

            _character.SetAttributes(attributes);

            _character.Events.Should().Contain(x => x.Is(typeof(AttributesSet)));
            _character.Attributes.Should().Be(attributes);
        }

        #endregion
    }
}
