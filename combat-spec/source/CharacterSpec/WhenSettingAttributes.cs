using System.Collections.Generic;
using EventSourcingDemo.Combat;
using FluentAssertions;
using Xunit;

namespace EventSourcingDemo.CombatSpec.CharacterSpec
{
    public class WhenSettingAttributes
    {
        #region Core

        private readonly Character _character = ObjectProvider.CreateCharacter();

        #endregion

        #region Public Interface

        public static IEnumerable<object[]> GetSetAttributes()
        {
            yield return new object[] { new Attributes(20, 0, 20, 10, 2, 20) };
            yield return new object[] { new Attributes(2, 3, 4, 5, 6, 7) };
        }

        #endregion

        #region Test Methods

        [Fact]
        public void GivenIdenticalAttributes_ThenReturnNoOpError()
        {
            var error = _character.Set(Attributes.Default).Error;

            error.Should().Be(Character.NoOp());
        }

        [Theory]
        [MemberData(nameof(GetSetAttributes))]
        public void ThenReturnAttributesSetEvent(Attributes attributes)
        {
            var @event = _character.Set(attributes).Value;

            @event.Attributes.Should().Be(attributes);
        }

        #endregion
    }
}
