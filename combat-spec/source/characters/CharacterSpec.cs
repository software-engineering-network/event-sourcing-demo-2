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
            _character = new Character("Mario");
        }

        #endregion

        #region Test Methods

        [Theory]
        [InlineData("Mario")]
        [InlineData("Mario", 20, 0, 20, 10, 2, 20)]
        public void WhenCreating(
            string name,
            ushort attack = default,
            ushort defense = default,
            ushort hitPoints = default,
            ushort magicAttack = default,
            ushort magicDefense = default,
            ushort speed = default
        )
        {
            var attributes = new Attributes(
                attack,
                defense,
                hitPoints,
                magicAttack,
                magicDefense,
                speed
            );

            var character = new Character(name, attributes);

            character.Events.Should().Contain(x => x.Is(typeof(CharacterCreated)));
            character.Id.Should().NotBeEmpty();
            character.Name.Should().Be("Mario");
            character.Attributes.Should().BeEquivalentTo(attributes);
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
        public void WhenRenaming()
        {
            _character.Rename("Maria");

            _character.Events.Should().Contain(x => x.Is(typeof(Renamed)));
            _character.Name.Should().Be("Maria");
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
