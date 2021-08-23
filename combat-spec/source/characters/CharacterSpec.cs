using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;
using static EventSourcingDemo.Combat.Attributes;

namespace EventSourcingDemo.CombatSpec
{
    public class CharacterSpec
    {
        #region Core

        private readonly Character _character;

        public CharacterSpec()
        {
            _character = new Character("Mario");
        }

        #endregion

        #region Test Methods

        [Fact]
        public void WhenCreating()
        {
            _character.Events.Should().Contain(x => x.Is(typeof(CharacterCreated)));
            _character.Id.Should().NotBeEmpty();
            _character.Name.Should().Be("Mario");
            _character.Attributes.Should().BeEquivalentTo(Default);
        }

        [Fact]
        public void WhenModifyingAttributes()
        {
            _character
                .ModifyAttributes(new(1, 1, 1, 1, 1, 1))
                .ModifyAttributes(new(2, 2, 2, 2, 2, 2));

            _character.Events.Should().Contain(x => x.Is(typeof(AttributesModified)));
            _character.Attributes.Should().BeEquivalentTo(new Attributes(3, 3, 3, 3, 3, 3));
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
