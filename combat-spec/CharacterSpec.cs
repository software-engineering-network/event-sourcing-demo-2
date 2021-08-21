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

        #endregion
    }
}
