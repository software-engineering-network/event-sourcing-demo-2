using System.Collections.Generic;
using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;

namespace EventSourcingDemo.CombatSpec.CharacterSpec
{
    public class WhenModifyingAttributes
    {
        #region Core

        private readonly Character _character;

        public WhenModifyingAttributes()
        {
            _character = ObjectProvider.CreateCharacter();
            _character.Set(new Attributes(20, 0, 20, 10, 2, 20));
        }

        #endregion

        #region Public Interface

        public static IEnumerable<object[]> GetAttributes()
        {
            yield return new object[] { new Attributes(10, 0, 0, 0, 0, 0) };
        }

        #endregion

        #region Test Methods

        [Fact]
        public void GivenNoDelta_ThenReturnNoOpError()
        {
            var error = _character.Add(Attributes.Default).Error;

            error.Should().Be(Character.NoOp());
        }

        [Theory]
        [MemberData(nameof(GetAttributes))]
        public void ThenReturnAttributesModifiedEvent(Attributes delta)
        {
            var @event = _character.Add(delta).Value;

            @event.Delta.Should().Be(delta);
        }

        #endregion
    }
}
